using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedFSM : FSM 
{
    private List<FSMState> fsmStates;
    private FSMStateID currentStateID;
    public FSMStateID CurrentStateID { get { return currentStateID; } }

    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public AdvancedFSM()
    {
        fsmStates = new List<FSMState>();
    }
    public void AddFSMState(FSMState fsmState)
    {
        // 检查状态是否为空
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }

        // 检测状态是否在列表中
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
                return;
            }
        }

        //添加状态进列表
        fsmStates.Add(fsmState);
    }

    
    //删除列表中状态     
    public void DeleteState(FSMStateID fsmState)
    {
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
    }

    /// <summary>
    /// 当前状态通过条件转换
    /// </summary>
    public void PerformTransition(Transition trans)
    {
        FSMStateID id = currentState.GetOutputState(trans);

        // 更新当前状态和当前状态id		
        currentStateID = id;
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}
