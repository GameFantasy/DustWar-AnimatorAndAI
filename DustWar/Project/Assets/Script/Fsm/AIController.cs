using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : AdvancedFSM 
{
    //名字
    private string characterName;
    //初始化
    protected override void Initialize()
    {
        AnimationManager.Instance.animator = GetComponent<Animator>();
        characterName = name;
        AnimationManager.Instance.AddAnimMoion("Knight","idle",false);
        AnimationManager.Instance.AddAnimMoion("Knight","run", false);
        AnimationManager.Instance.AddTransition("idle", "run", "isRun");
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
