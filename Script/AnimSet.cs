using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract  class AnimSet:MonoBehaviour
{
    public abstract string GetIdleAnim();
    public abstract string GetMoveAnim(E_MotionType motion, E_MoveType move, E_WeaponType weapon, E_WeaponState weaponState);
    public abstract string GetShowWeaponAnim();
    public abstract string GetHideWeaponAnim();
}

