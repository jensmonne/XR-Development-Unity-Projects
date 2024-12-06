using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;

    public bool moving;

    private void FixedUpdate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", moving ? 1f : 0f);
    }
}