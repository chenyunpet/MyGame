using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum E_AnimFSMTypes
{
    Player,
    Boss,
}

public  class AnimCompoent:MonoBehaviour,IActionHandle
{
    public E_AnimFSMTypes TypeOfFSM;
    private AnimFSM FSM;
    private Animation Animation;
    private Agent Owner;
    public  void Awake()
    {
        Animation = GetComponent<Animation>();
        Owner = GetComponent<Agent>();
        switch (TypeOfFSM)
        {
            case E_AnimFSMTypes.Player:
                FSM = new AnimFSMPlayer(Animation, Owner);
                break;
            case E_AnimFSMTypes.Boss:
                break;
            default:
                Debug.LogError(this.name+"unknow type of FSM. Type:"+TypeOfFSM.ToString());
                break;
        }

    }
    void Start()
    {
        FSM.Initialize();
        Owner.BlackBoard.AddActionHandle(this);
    }
    void Update()
    {
        FSM.Update();
    }
    public void Activate()
    {
        FSM.Initialize();
    }
    public void Deactivate()
    {
        FSM.Reset();
    }

    public void  HandleAction(AgentAction _action)
    {
        if(!_action.IsFailed())
        {
            FSM.DoAction(_action);
        }

    }
}

