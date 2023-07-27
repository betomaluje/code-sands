using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCRandomAnimation : MonoBehaviour
{
    [SerializeField] private string[] availableAnimations;
    [SerializeField] private int defaultAnimationIndex = -1;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        var exists = availableAnimations.ElementAtOrDefault(defaultAnimationIndex) != null;

        if (exists)
        {
            animator.Play(availableAnimations[defaultAnimationIndex]);
        }
        else
        {
            animator.Play(availableAnimations[Random.Range(0, availableAnimations.Length)]);
        }
    }
}
