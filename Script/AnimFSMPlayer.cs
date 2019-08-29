using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class AnimFSMPlayer:AnimFSM
{
    enum E_AnimState
    {
        E_Idle,
        E_Move,
        E_Attack,
    }

    public AnimFSMPlayer(Animation _anims,Agent _owner):base(_anims,_owner)
    {

    }
    public override void Initialize()
    {
        AnimStates.Add(new AnimStateIdle(AnimEngine, Owner));
        AnimStates.Add(new AnimStateMove(AnimEngine, Owner));
        DefaultAnimState = AnimStates[(int)E_AnimState.E_Idle];
        base.Initialize();

    }
    public override void DoAction(AgentAction _action)
    {
        if(CurrentAnimState.HandleNewAction(_action))
        {
            NextAnimState = null;
        }
        else
        {
            //if (_action is AgentActionIdle)
            //{
            //    NextAnimState = AnimStates[(int)(E_AnimState.E_Idle)];
            //}
            if (_action is AgentActionMove)
            {
                NextAnimState = AnimStates[(int)(E_AnimState.E_Move)];
            }
            //else
            //{
            //    Debug.Log("Not Find AnimState");
            //}
            if(null!=NextAnimState)
            {
                ProgressToNextStage(_action);
            }

        }
        //base.DoAction(_action);
    }
}

