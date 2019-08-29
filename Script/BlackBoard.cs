using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface IActionHandle
{
    void HandleAction(AgentAction _action);
}
public class BlackBoard
{
    private List<AgentAction> _ActiveActions = new List<AgentAction>();
    private List<IActionHandle> _ActionHandles = new List<IActionHandle>();
    //黑板拥有者
    public Agent Owner;
    public GameObject myGameObject;
    //最大冲刺速度
    public float MaxSprintSpeed = 8;
    //最大奔跑速度
    public float MaxRunSpeed = 4;
    //最大行走速度
    public float MaxWalkSpeed = 1.5f;

    public float Speed = 0;
    public Vector3 MoveDir= Vector3.zero;
    //运动类型
   
    public E_MotionType MotionType = E_MotionType.None;
    public E_WeaponState WeaponState=E_WeaponState.Ready;
    public E_WeaponType WeaponSelected = E_WeaponType.Katana;
    public E_WeaponType WeaponToSelected = E_WeaponType.None;
    public float WeaponRange = 3;
    public float SpeedSmooth = 2.0f;
    public float RotationSmooth = 2.0f;
    public float MoveSpeedModifier = 1;//移动速度的倍数限制
    public Vector3 DesiredPosition;
    public Vector3 DesiredDirection;
    public bool DontUpdate = true;
    public  void AddAction(AgentAction _action)
    {
        _ActiveActions.Add(_action);
        //todo:call HandleAction
        for (int i = 0; i < _ActionHandles.Count; i++)
        {
             Debug.Log("_ActionHandles.Count="+_ActionHandles.Count);
            _ActionHandles[i].HandleAction(_action);
        }
    }
    public AgentAction GetAction(int index)
    {
        return _ActiveActions[index];
    }
    public int ActionCount
    {
        get { return _ActiveActions.Count; }
    }

    public void AddActionHandle(IActionHandle _handle)
    {
        for(int i=0;i<_ActionHandles.Count;i++)
        {
            if (_ActionHandles[i] == _handle)
                return;
        }
        _ActionHandles.Add(_handle);
    }
    public void RemoveActionHandle(IActionHandle _handle)
    {
        _ActionHandles.Remove(_handle);
    }
    public void Update()
    {
        for (int i = 0; i < _ActiveActions.Count; i++)
        {
            if (_ActiveActions[i].IsActive())
                continue;
            ActionDone(_ActiveActions[i]);
            _ActiveActions.RemoveAt(i);
            return;
        }
    }
    private  void ActionDone(AgentAction _acion)
    {
        // todo...
        AgentActionFactory.Return(_acion);

    }
}

