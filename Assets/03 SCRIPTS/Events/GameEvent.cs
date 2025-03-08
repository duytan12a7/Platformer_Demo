using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{

    public static event Action OnHealthChanged;
    public static void CallOnHealthChanged() => OnHealthChanged?.Invoke();

    public static event Action<int> OnGainExp;
    public static void CallOnGainExp(int exp) => OnGainExp?.Invoke(exp);

    public static event Action OnExpChanged;
    public static void CallOnExpChanged() => OnExpChanged?.Invoke();
}
