using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnimSetPlayer:AnimSet
{
    public override string GetIdleAnim()
    {
        return "idle";
    }
    public override string GetMoveAnim(E_MotionType motion, E_MoveType move, E_WeaponType weapon, E_WeaponState weaponState)
    {
        if(E_WeaponState.NotInHands== weaponState)
        {
            if (E_MotionType.Walk == motion)
                return "walk";
            return "run";
        }
        if(E_MotionType.Walk==motion)
            return "walkSword";
        return "runSword";
    }
    public override string GetShowWeaponAnim()
    {
        return "showSwordRun";
    }
    public override string GetHideWeaponAnim()
    {
        return "hideSwordRun";
    }
   
}

