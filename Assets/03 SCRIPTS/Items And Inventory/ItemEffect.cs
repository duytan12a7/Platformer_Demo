using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect")]
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ExcuteEffect();
}
