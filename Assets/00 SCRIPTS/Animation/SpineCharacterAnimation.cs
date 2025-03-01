using Spine.Unity;

public class SpineCharacterAnimation : ICharacterAnimation
{
    private SkeletonAnimation skeletonAnimation;

    public SpineCharacterAnimation(SkeletonAnimation skeletonAnimation)
    {
        this.skeletonAnimation = skeletonAnimation;
    }

    public void PlayAnimation(string animationName, bool loop) => skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);

    public void SetSpeedAnimation(float speed) => skeletonAnimation.timeScale = speed;

    public void SetTrigger(string triggerName) => skeletonAnimation.AnimationState.SetAnimation(0, triggerName, false);

    public void StopAnimation() => skeletonAnimation.AnimationState.ClearTracks();
}
