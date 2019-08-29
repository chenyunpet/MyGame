using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AnimState:System.Object
{
    protected Animation AnimEngine;
    protected Agent Owner;
    protected Transform Transform;
    protected Transform RootTransform;
    private bool m_IsFinished = true;
    public AnimState(Animation _anims,Agent _owner)
    {
        AnimEngine = _anims;
        Owner = _owner;
        Transform = Owner.transform;
        RootTransform = Transform.Find("root");
    }
    protected virtual void Initialize(AgentAction _action)
    {

    }
    public virtual void OnActivate(AgentAction _action)
    {
        SetFinished(false);

        Initialize(_action);

    }
    public virtual void OnDeactivate()
    {
        

    }
    public virtual void Update()
    {

    }
    public virtual bool IsFinished()
    {
        return m_IsFinished;
    }
    public virtual void SetFinished(bool _finished )
    {
        m_IsFinished = _finished;
    }
    public virtual bool HandleNewAction(AgentAction _action)
    {
        return false;

    }
    public virtual void Release()
    {
        SetFinished(true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="fadeInTime"></param>
    protected void CrossFade(string anim,float fadeInTime)
    {
        Debug.Log("anim="+anim);
        if(AnimEngine.IsPlaying(anim))
        {
            Debug.Log("anim=" + anim);
            AnimEngine.CrossFadeQueued(anim,fadeInTime,QueueMode.PlayNow);
        }
        else
        {
            Debug.Log("anim=" + anim);
            AnimEngine.CrossFade(anim,fadeInTime);
        }

    }
    protected bool Move(Vector3 velocity)
    {
        Vector3 old = Transform.position;
        CollisionFlags flags = Owner.CharacterController.Move(velocity);
        return true;
    }
}

