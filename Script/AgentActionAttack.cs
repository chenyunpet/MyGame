using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 攻击
/// </summary>
public  class AgentActionAttack:AgentAction
{
    public Agent Target;
    public E_AttackType AttackType = E_AttackType.None;
    public Vector3 AttackDir;
    public bool IsHit = false;
    //攻击是否完成
    public bool AttackPhaseDone = false;
    public AgentActionAttack():base(AgentActionFactory.E_Type.E_ATTACK)
    {

    }
    public override void Reset()
    {
        base.Reset();
        Target = null;
        IsHit = false;
        AttackPhaseDone = false;
        AttackType = E_AttackType.None;
    }
    public override string ToString()
    {
        string stNoTarget = (Target != null) ? Target.name : "No Target";
        return "[AgentActionAttack]"+ stNoTarget+" " +AttackType.ToString()+" "+Status.ToString();
    }
}

