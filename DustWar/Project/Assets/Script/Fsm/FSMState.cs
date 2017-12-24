using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    protected FSMStateID stateID;
    public FSMStateID ID { get { return stateID; } }
    //移动速度
    protected float curMoveSpeed;
    protected float curRotSpeed;
    //向字典添加-对
    public void AddTransition(Transition transition, FSMStateID id)
    {
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }

    /// <summary>
    /// 从字典删除状态    
    /// </summary>
    public void DeleteTransition(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this StateList");
    }


    /// <summary>
    /// 根据ID获得当前状态
    /// </summary>
    public FSMStateID GetOutputState(Transition trans)
    {
        return map[trans];
    }

    /// <summary>
    ///确定是否转换状体，要转换的状态
    /// </summary>
    public abstract void Reason(Transform player, Transform npc);

    /// <summary>
    /// 定义角色的行为
    /// </summary>
    public abstract void Act(Transform player, Transform npc);

}
