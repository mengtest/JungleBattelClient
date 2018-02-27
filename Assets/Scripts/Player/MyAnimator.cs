using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ZJD
{
    public class MyAnimator
    {
        public static readonly State State=new State();
        public static readonly Trigger Trigger=new Trigger();
        public static readonly Float Float=new Float();
    }

    public class State
    {
        public readonly string Grounded = "Grounded";
        public readonly string Attack = "Attack";
    }


    public class Trigger
    {
        public readonly int Attack = Animator.StringToHash("Attack");
    }

    public class Float
    {
        public readonly int Forward = Animator.StringToHash("Forward");
    }
}