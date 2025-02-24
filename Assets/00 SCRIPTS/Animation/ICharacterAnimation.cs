public interface ICharacterAnimation
{
    void PlayAnimation(string animationName, bool loop);
    void SetTrigger(string triggerName);
    void StopAnimation();
}
