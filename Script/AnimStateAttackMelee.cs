using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public   class AnimStateAttackMelee:AnimState
{
    enum E_State
    {
        E_Preparing,
        E_Attacking,
        E_Finished,

    }
    AgentActionAttack Action;
    //AnimData
    //攻击动作时间长度
    float AttackPhaseTime;
    //击中时间
    float HitTime;
    //当前状态结束时间
    float EndOfStateTime;
    //暴击
    bool Critical = false;
    //击倒
    bool KnockDown = false;
    Vector3 StartPosition;
    Vector3 FinalPosition;
    Quaternion StartRotation;
    Quaternion FinalRotation;
    float MoveTime;
    float CurrentMoveTime;
    float RotationTime;
    float CurrentRotationTime;
    /// <summary>
    /// 移动到了
    /// </summary>
    bool PositionOk = false;
    bool RotationOk = false;
    E_State State;

    public AnimStateAttackMelee(Animation _anims, Agent _owner):base(_anims,_owner)
    {

    }
    public override void OnActivate(AgentAction _action)
    {
        base.OnActivate(_action);
    }
    public override void OnDeactivate()
    {
        if(null!=Action)
        {
            Action.SetSuccess();
            Action = null;
        }
        base.OnDeactivate();
    }
    public override void Update()
    {
        if(State==E_State.E_Preparing)
        {
            bool dontMove = false;
            if(RotationOk==false)
            {
                CurrentRotationTime += Time.deltaTime;
                if(CurrentRotationTime>=RotationTime)
                {
                    CurrentRotationTime = RotationTime;
                    RotationOk = true;
                   
                }
                else
                {
                    float progress = CurrentRotationTime / RotationTime;
                    Quaternion q = Quaternion.Lerp(StartRotation,FinalRotation,progress);
                    Owner.Transform.rotation = q;
                    if(Quaternion.Angle(q,FinalRotation)>20.0f)
                    {
                        dontMove = true;
                    }
                }
            }
            if(dontMove==false&&PositionOk == false)
            {
                CurrentMoveTime += Time.deltaTime;
                if(CurrentMoveTime>=MoveTime)
                {
                    CurrentMoveTime = MoveTime;
                    PositionOk = true;
                    CurrentMoveTime = 0;
                }
                if (CurrentMoveTime > 0)
                {
                    float progress = CurrentMoveTime / MoveTime;
                    Vector3 finalPos = Mathfx.Hermite(StartPosition, FinalPosition, progress);//todo
                    if (Move(finalPos - Transform.position) == false)
                    {
                        PositionOk = true;
                    }
                }
                
            }
            if (PositionOk && RotationOk)
            {
                State = E_State.E_Attacking;
                //
                PlayAnim();
            }
        }
        else if(State== E_State.E_Attacking)
        {
            CurrentMoveTime += Time.deltaTime;
            if (AttackPhaseTime < Time.timeSinceLevelLoad)
            {
                State = E_State.E_Finished;
            }
            if(CurrentMoveTime>=MoveTime)
            {
                CurrentMoveTime = MoveTime;
            }
            if(CurrentMoveTime>0&& CurrentMoveTime<=MoveTime)
            {
                float progress = CurrentMoveTime / MoveTime;
                Vector3 finalPos = Mathfx.Hermite(StartPosition, FinalPosition, progress);//todo
                if (Move(finalPos - Transform.position) == false)
                {
                    CurrentMoveTime = MoveTime;
                }

            }
            if(Action.IsHit==false&&HitTime<=Time.timeSinceLevelLoad)
            {
                Action.IsHit = true;
            }
        }
        else if(State==E_State.E_Finished)
        {
            Action.AttackPhaseDone = true;
            Release();
        }
        //base.Update();
    }
    /// <summary>
    /// 接受命令
    /// </summary>
    /// <param name="_action"></param>
    /// <returns></returns>
    public override bool HandleNewAction(AgentAction _action)
    {
        if(_action as AgentActionAttack!=null)
        {
            if(null!=Action)
            {
                Action.SetSuccess();
            }
            Initialize(_action);
            return true;
        }
        return false;
    }
    protected override void Initialize(AgentAction _action)
    {
        base.Initialize(_action);
        SetFinished(false);
        State = E_State.E_Preparing;
        Owner.BlackBoard.MotionType = E_MotionType.Attack;
        Action = _action as AgentActionAttack;
        Action.IsHit = false;

        StartPosition = Transform.position;
        StartRotation = Transform.rotation;
        float angle = 0;
        float distance = 0;
        if(Action.Target!=null)
        {
            Vector3 dir = Action.Target.Position - Transform.position;
            distance = dir.magnitude;
            if(distance>0.1f)
            {
                dir.Normalize();
                angle = Vector3.Angle(Transform.forward, dir);
                //todo : add back logic
            }
            else
            {
                dir = Transform.forward;
            }
            FinalRotation.SetLookRotation(dir);

            if(distance<Owner.BlackBoard.WeaponRange)
            {
                FinalPosition = StartPosition;
            }
            else
            {
                FinalPosition = Action.Target.Transform.position - dir * Owner.BlackBoard.WeaponRange;
            }
            MoveTime = (FinalPosition - StartPosition).magnitude / 10.0f;
            RotationTime = angle / 720.0f;
        }
        else
        {
            FinalRotation.SetLookRotation(Action.AttackDir);
            RotationTime = Vector3.Angle(Transform.forward,Action.AttackDir)/720.0f;
            MoveTime = 0;
        }
        PositionOk = MoveTime == 0;
        RotationOk = RotationTime == 0;
        CurrentMoveTime = 0;
        CurrentRotationTime = 0;
    }
    private void PlayAnim()
    {
        string animName = "attackx";
        CrossFade(animName,0.2f);
        HitTime = Time.timeSinceLevelLoad + 1.0f;
        StartPosition = Transform.position;
        FinalPosition = StartRotation * Transform.forward * 1.0f;
        //攻击位移结束时间-攻击开始时间
        MoveTime = 1.0f;
        EndOfStateTime = Time.timeSinceLevelLoad+AnimEngine[animName].length*0.9f;
        //攻击动作前摇时间
        CurrentMoveTime = -1.0f;
        //攻击结束时间
        AttackPhaseTime = Time.deltaTime + 3.0f;


    }
}

