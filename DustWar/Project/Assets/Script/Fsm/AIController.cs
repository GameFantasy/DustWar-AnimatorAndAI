using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : AdvancedFSM 
{
    //初始化
    public Animator anim;
    protected override void Initialize()
    {
        anim=GetComponent<Animator>();
        AnimationManager.Instance.addAnimStates("wolf", "idle", anim);
    }


    protected override void FSMUpdate()
    {
    }

    protected override void FSMFixedUpdate()
    {
    }

    public void SetTransition(Transition t)
    {
    }

    private void ConstructFSM()
    {
    }
}
