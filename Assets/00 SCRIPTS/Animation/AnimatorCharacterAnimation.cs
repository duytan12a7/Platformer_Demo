using UnityEngine;

public class AnimatorCharacterAnimation : ICharacterAnimation
{
    private Animator animator;

    public AnimatorCharacterAnimation(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayAnimation(string animationName, bool loop) => animator.SetBool(animationName, loop);

    public void SetSpeedAnimation(float speed) => animator.speed = speed;

    public void SetTrigger(string triggerName) => animator.SetTrigger(triggerName);

    public void StopAnimation() => animator.Play("Idle");
}
