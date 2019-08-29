using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimStateIdle:AnimState
{
    float TimeToFinishWeaponAction=0;
    AgentAction WeaponAction;
    public AnimStateIdle(Animation _anims,Agent _owner):base(_anims,_owner)
    {

    }
    public override void Release()
    {
        SetFinished(true);
    }
    public override bool HandleNewAction(AgentAction _action)
    {
        string animName;
        float delyTime;
        float fadeInTime;
        if(_action is AgentActionWeaponShow)
        {
            if((_action as AgentActionWeaponShow).Show == true)
            {
                //show Weapon
                animName = Owner.AnimSet.GetShowWeaponAnim();
                delyTime = 0.8f;
                fadeInTime = 0.2f;
            }
            else
            {
                //hide Weapon
                animName = Owner.AnimSet.GetHideWeaponAnim();
                delyTime = 0.9f;
                fadeInTime = 0.1f;
            }
            Debug.Log("animName="+animName);
            TimeToFinishWeaponAction = Time.realtimeSinceStartup + AnimEngine[animName].length * delyTime;
            CrossFade(animName, fadeInTime);
            WeaponAction = _action;
        }
        return false;
    }
    public override void Update()
    {
       
        if (WeaponAction!=null&& TimeToFinishWeaponAction<Time.timeSinceLevelLoad)
        {
            WeaponAction.SetSuccess();
            WeaponAction = null;
            Debug.Log("Is Idle");
            PlayIdleAnim();
        }
    }
    void PlayIdleAnim()
    {
        // player AnimIdle
        string animName = Owner.AnimSet.GetIdleAnim();
        CrossFade(animName,0.2f);
    }
    protected override void Initialize(AgentAction _action)
    {
        base.Initialize(_action);
        Owner.BlackBoard.MoveDir = Vector3.zero;
        Owner.BlackBoard.Speed = 0;
        if(WeaponAction==null)
        {
            Debug.Log("Is Idle.........");
            PlayIdleAnim();
        }
    }
}

