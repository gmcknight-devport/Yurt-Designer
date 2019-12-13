using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager 
{
    public enum States
    {
        Outside, Inside, Build
    }

    public static States state = States.Outside;

    public static void ChangeState(States s)
    {
        state = s;
    }
}
