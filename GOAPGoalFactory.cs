using System;

public enum E_GOAPGoals
{
	E_INVALID = -1,
	E_ORDER_ATTACK,
	E_ORDER_DODGE,
	E_ORDER_USE,
	E_GOTO,
	E_COMBAT_MOVE_LEFT,
	E_COMBAT_MOVE_RIGHT,
	E_COMBAT_MOVE_FORWARD,
	E_COMBAT_MOVE_BACKWARD,
	E_LOOK_AT_TARGET,
	E_KILL_TARGET,
	E_DODGE,
	E_DO_BLOCK,
	E_ALERT,
	E_CALM,
	E_USE_WORLD_OBJECT,
	E_PLAY_ANIM,
	E_IDLE_ANIM,
	E_REACT_TO_DAMAGE,
	E_BOSS_ATTACK,
	E_TELEPORT,
	E_COUNT,
}

class GOAPGoalFactory : System.Object
{
    public static GOAPGoal Create(E_GOAPGoals type, Agent owner)
    {
        GOAPGoal g;
        switch (type)
        {
            case E_GOAPGoals.E_GOTO:
                g = new GOAPGoalGoTo(owner);
                break;
            case E_GOAPGoals.E_PLAY_ANIM:
                g = new GOAPGoalPlayAnim(owner);
                break;
            case E_GOAPGoals.E_IDLE_ANIM:
                g = new GOAPGoalIdleAction(owner);
                break;
            default:
                return null;
        }

        g.InitGoal();
        return g;
    }

}
