using UnityEngine;

public class PauseMenuAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // This will search this GameObject and all its children for an Animator component.
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject or any of its children.");
        }
    }

    public void StartAnimation()
    {
        if (animator != null)
        {
            animator.Play("PauseAnimation");
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }
    }
}
