using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AnimCompoent))]
[RequireComponent(typeof(AnimSetPlayer))]
public class CompinentPlayer:MonoBehaviour
{
    private Agent Agent;
    Vector3 MoveDirection;
    void Start()
    {
        Agent = GetComponent<Agent>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveDirection = new Vector3(h,0,v);


        if(MoveDirection != Vector3.zero)
        {
            MoveDirection.Normalize();
            CreateMove(MoveDirection);
        }
        else
        {
            CreateIdle();
        }
        if(Input.GetMouseButtonDown(0))
        {
            //CreateWeaponShow();
            CreateAttack();
        }
    }
    void CreateMove(Vector3 _moveDirection)
    {
        Agent.BlackBoard.DesiredDirection = _moveDirection;
        Agent.BlackBoard.DesiredPosition = Agent.Position;
        AgentAction _action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_MOVE);
        Agent.BlackBoard.AddAction(_action);
    }
    void CreateIdle()
    {
        Agent.BlackBoard.DesiredPosition = Agent.Position;
        AgentAction _action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_IDLE);
        Agent.BlackBoard.AddAction(_action);
    }

    void CreateWeaponShow()
    {
        AgentActionWeaponShow _action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_WEAPON_SHOW) as AgentActionWeaponShow;
        _action.Show = true;
        Agent.BlackBoard.AddAction(_action);
    }
    void CreateAttack()
    {
        AgentActionAttack _action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_ATTACK) as AgentActionAttack;
        _action.AttackType= E_AttackType.X;
        if(MoveDirection!=Vector3.zero)
        {
            _action.AttackDir = MoveDirection;
        }
        else
        {
            _action.AttackDir = Agent.Transform.forward;

        }
        _action.Target = null;
        Agent.BlackBoard.AddAction(_action);
    }
}

