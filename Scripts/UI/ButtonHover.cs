using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Sounds")]
    [SerializeField] private AudioClip hover;
    [SerializeField] private AudioClip click;

    private readonly int ANIM_HOVER = Animator.StringToHash("Btn_Hover");
    private readonly int ANIM_UNHOVER = Animator.StringToHash("Btn_UnHover");

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnHover()
    {
        animator.Play(ANIM_HOVER);
        _audioSource.PlayOneShot(hover);
    }

    public void OnUnHover()
    {
        animator.Play(ANIM_UNHOVER);
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(click);
    }
}