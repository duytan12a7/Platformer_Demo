public interface ICharacterAnimation
{
    void PlayAnimation(string animationName, bool isActive);
    void SetSpeedAnimation(float speed);
    void SetTrigger(string triggerName);
    void StopAnimation(string animationName, bool isActive);
}
