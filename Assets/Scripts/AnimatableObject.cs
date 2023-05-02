
using UnityEngine;

public class AnimatableObject : MonoBehaviour
{
    public Animator animator;
    public bool isHit;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isHit) PlayAnimation();
        else StopAnimation();
    }

    public void PlayAnimation()
    {
        animator.SetBool("Play", true);
    }

    public void StopAnimation()
    {
        animator.SetBool("Play", false);
    }
}
