using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    public IdleState() 
    {
        stateID = FSMStateID.Idle;
        curMoveSpeed = 80.0f;
        curRotSpeed = 6.0f;
    }
    public override void Reason(Transform npc, Transform player)
    {
        if (Vector3.Distance(npc.position, player.position)<=40.0f) 
        {
            Debug.Log("发现敌人");
            npc.GetComponent<AIController>().SetTransition(Transition.FindEnemy);
        }
    }
    public override void Act( Transform npc,Transform player)
    {
        AnimationManager.Instance.AddAnimMoion(npc.gameObject.name, "idle", false);
        
    }
}
