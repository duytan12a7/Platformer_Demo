using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action OnFlipped;

    public static void CallOnFlipped() => OnFlipped?.Invoke();

    public static event Action OnHealthChanged;

    public static void CallOnHealthChanged() => OnHealthChanged?.Invoke();
}
