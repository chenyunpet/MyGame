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
            //CreateMove(MoveDirection);
            CreateOrderGoTo(MoveDirection);
        }
        else
        {
            //CreateIdle();
            CreateOrderStop();
        }
        if(Input.GetMouseButtonDown(0))
        {
            //CreateWeaponShow();
            //CreateAttack();
            CreateOrderAttack(E_AttackType.X);
        }
    }

    void CreateOrderGoTo(Vector3 _moveDirection)
    {
        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_GOTO);
        order.Direction = _moveDirection;
        order.Position = Agent.Position;
        order.MoveSpeedModifier = 1.0f;
        Agent.BlackBoard.AddOrder(order);
    }
    void CreateOrderStop()
    {
        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_STOPMOVE);
        Agent.BlackBoard.AddOrder(order);
    }
    void CreateOrderAttack(E_AttackType type)
    {
        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_ATTACK);
        order.AttackType = type;
        // todo ...移动方向像正前方
        order.Direction = transform.forward;
        //todo 攻击数据
        order.AnimAttackData = null;
        //todo... 攻击目标
        order.Target = null; 
        //todo ...根据条件添加
        Agent.BlackBoard.AddOrder(order);
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
        _action.Data = new AnimAttackData("attackO",0.5f,0.1f,1.0f,111,30);
        Agent.BlackBoard.AddAction(_action);
    }
}

