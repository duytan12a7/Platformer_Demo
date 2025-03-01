public interface ICharacterAnimation
{
    void PlayAnimation(string animationName, bool loop);
    void SetSpeedAnimation(float speed);
    void SetTrigger(string triggerName);
    void StopAnimation();
}
