using BerserkPixel.Prata;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomDialogRenderer : DialogRenderer
{
    [SerializeField] private GameObject container;
    [SerializeField] private Text authorText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Transform dialogLeftImage;
    [SerializeField] private Transform dialogRightImage;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("Camera settings")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private Transform playerTarget;
    public Transform otherTarget;

    private void Awake()
    {
        if (playerTarget == null)
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("Head");
    }

    public override void Show()
    {
        container.SetActive(true);
    }

    public override void Render(Dialog dialog)
    {
        dialogText.text = dialog.text;
        authorText.text = dialog.character.characterName;
        if (dialog.character.isPlayer)
        {
            dialogRightImage.gameObject.SetActive(true);
            dialogLeftImage.gameObject.SetActive(false);

            //cinemachineCamera.m_Follow = playerTarget;
            cinemachineCamera.m_LookAt = playerTarget;
        }
        else
        {
            dialogLeftImage.gameObject.SetActive(true);
            dialogRightImage.gameObject.SetActive(false);

            //cinemachineCamera.m_Follow = otherTarget;
            cinemachineCamera.m_LookAt = otherTarget;
        }

        if (dialog.choices.Count > 1)
        {
            RemoveChoices();
            GameObject firstChoice = null;
            foreach (var choice in dialog.choices)
            {
                GameObject choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
                choiceButton.GetComponentInChildren<Text>().text = choice;
                choiceButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DialogManager.Instance.MakeChoice(dialog.guid, choice);
                });

                if (firstChoice == null)
                {
                    firstChoice = choiceButton;
                }
            }

            FindObjectOfType<EventSystem>().firstSelectedGameObject = firstChoice;

            choicesContainer.gameObject.SetActive(true);
        }
        else
        {
            choicesContainer.gameObject.SetActive(false);
        }
    }

    public override void Hide()
    {
        RemoveChoices();
        container.SetActive(false);
    }

    private void RemoveChoices()
    {
        if (choicesContainer.childCount > 0)
        {
            foreach (Transform child in choicesContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
