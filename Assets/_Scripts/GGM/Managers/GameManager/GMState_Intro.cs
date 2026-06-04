// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;

public class GMState_Intro : StateBase
{
    public override void OnStateEnter(params object[] o)
    {
        base.OnStateEnter(o);
        Debug.Log("Intro State");
    }

    public override void OnStateStay()
    {
        base.OnStateStay();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
