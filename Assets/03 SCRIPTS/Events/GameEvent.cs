using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{

    public static event Action OnHealthChanged;

    public static void CallOnHealthChanged() => OnHealthChanged?.Invoke();
}
