using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine 
{
    /// <summary>  
    /// 存储所有注册进来的状态。key是状态ID，value是状态对象  
    /// </summary>  
    private Dictionary<int, IState> m_dictState;  
    /// <summary>  
    /// 当前运行的状态  
    /// </summary>  
    private IState m_curState;
  
    public GameStateMachine()  
    {  
        m_curState = null;  
        m_dictState = new Dictionary<int, IState>();
    }  
  
    /// <summary>  
    /// 注册一个状态  
    /// </summary>  
    /// <param name="state">要注册的状态</param>  
    /// <returns>成功返回true，如果此状态ID已存在或状态为NULL，则返回false</returns>  
    public bool RegistState(IState state)  
    {  
        if (null == state)  
        {  
            Debug.LogWarning("StateMachine::RegistState->state is null");  
            return false;  
        }  
  
        if (m_dictState.ContainsKey(state.GetStateID()))  
        {  
            Debug.LogWarning("StateMachine::RegistState->state had exist! state id=" + state.GetStateID());  
            return false;  
        }  
  
        m_dictState[state.GetStateID()] = state;  
  
        return true;  
    }  
  
    /// <summary>  
    /// 尝试获取一个状态  
    /// </summary>  
    /// <param name="iStateId"></param>  
    /// <returns></returns>  
    public IState GetState(int iStateId)  
    {  
        IState ret = null;  
        m_dictState.TryGetValue(iStateId, out ret);  
        return ret;  
    }  
  
    /// <summary>  
    /// 停止当前正在运行的状态, 切换到空状态  
    /// </summary>  
    public void StopState(object param1, object param2)  
    {  
        if (null == m_curState)  
        {  
            return;  
        }  
  
        m_curState.OnLeave();  
  
        m_curState = null;  
    }  
  
    /// <summary>  
    /// 取消状态的注册  
    /// </summary>  
    /// <param name="iStateID">要取消的状态ID</param>  
    /// <returns>如果找不到状态或状态正在运行，则会返回false</returns>  
    public bool CancelState(int iStateID)  
    {  
        if (!m_dictState.ContainsKey(iStateID))  
        {  
            return false;  
        }  
  
        if (null != m_curState && m_curState.GetStateID() == iStateID)  
        {  
            return false;  
        }  
  
        return m_dictState.Remove(iStateID);  
    }  
  
    public delegate void BetweenSwitchState(IState from, IState to, object param1, object param2);  
  
    /// <summary>  
    /// 在切换状态之间回调  
    /// </summary>  
    public BetweenSwitchState BetweenSwitchStateCallBack { get; set; }  
  
    /// <summary>  
    /// 切换状态  
    /// </summary>  
    /// <param name="iNewStateID">要切换的新状态</param>  
    /// <returns>如果找不到新的状态，或者新旧状态一样，返回false</returns>  
    public bool SwitchState(int iNewStateID, object param1, object param2)  
    {  
        //状态一样，不做转换//  
        if (null != m_curState && m_curState.GetStateID() == iNewStateID)  
        {  
            return false;  
        }  
  
        IState newState = null;  
        m_dictState.TryGetValue(iNewStateID, out newState);  
        if (null == newState)  
        {  
            return false;  
        }  
  
        IState oldState = m_curState;  
  
        if (null != oldState)  
        {  
            oldState.OnLeave();  
        }  
  
        //if (BetweenSwitchStateCallBack != null) BetweenSwitchStateCallBack(oldState, newState, param1, param2);  
  
        m_curState = newState;  
  
        if (null != newState)  
        {  
            newState.OnEnter();  
        }  
  
        return true;  
    }  
  
    /// <summary>  
    /// 获取当前状态  
    /// </summary>  
    /// <returns></returns>  
    public IState GetCurState()  
    {  
        return m_curState;  
    }  
  
    /// <summary>  
    /// 获取当前状态ID  
    /// </summary>  
    /// <returns></returns>  
    public int GetCurStateID()  
    {  
        IState state = GetCurState();  
        return (null == state) ? 0 : state.GetStateID();  
    }  
  
    /// <summary>  
    /// 判断当前是否在某个状态下  
    /// </summary>  
    /// <param name="iStateID"></param>  
    /// <returns></returns>  
    public bool IsInState(int iStateID)  
    {  
        if (null == m_curState)  
        {  
            return false;  
        }  
  
        return m_curState.GetStateID() == iStateID;  
    }  
  
    /// <summary>  
    /// 每帧的更新回调  
    /// </summary>  
    public void OnUpdate()  
    {  
        if (null != m_curState)  
        {  
            m_curState.OnUpdate();  
        }  
    }  
  
    /// <summary>  
    /// 每帧的更新回调  
    /// </summary>  
    public void OnFixedUpdate()  
    {  
        if (null != m_curState)  
        {  
            m_curState.OnFixedUpdate();  
        }  
    }  
  
    /// <summary>  
    /// 每帧的更新回调  
    /// </summary>  
    public void OnLateUpdate()  
    {  
        if (null != m_curState)  
        {  
            m_curState.OnLateUpdate();  
        }  
    }  
}
