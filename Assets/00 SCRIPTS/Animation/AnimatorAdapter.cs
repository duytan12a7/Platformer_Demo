using UnityEngine;

public class AnimatorAdapter : ICharacterAnimation
{
    private Animator animator;

    public AnimatorAdapter(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayAnimation(string animationName, bool loop)
    {
        animator.SetBool(animationName, loop);
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void StopAnimation()
    {
        animator.Play("Idle");
    }
}
