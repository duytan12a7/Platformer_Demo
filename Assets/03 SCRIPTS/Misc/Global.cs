using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static class AnimatorParams
    {
        public static string Idle = "Idle";
        public static string Move = "Move";
        public static string Dash = "Dash";
        public static string InAir = "InAir";
        public static string WallSlide = "WallSlide";
        public static string Land = "Land";
        public static string Attack = "Attack";
        public static string Hurt = "Hurt";
        public static string Die = "Die";
        public static string Heal = "Heal";
        public static string CounterAttack = "CounterAttack";
        public static string ComboCounter = "ComboCounter";
        public static string xVelocity = "xVelocity";
        public static string yVelocity = "yVelocity";

    }

    public static class LayerMask 
    {
        public static string Player = "Player";
        public static string Enemy = "Enemy";
    }
}
