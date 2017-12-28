using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public Player CurrPlayer { get; private set; } 
    public PlayerIdleState (Player currPlayer)  
    {
        CurrPlayer =currPlayer;  
    }  
    #region IState 成员  
    public int GetStateID()  
    {  
        return (int)Player.PlayerState.idle;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToIdle", true);
        //Debug.Log("进入idle状态"); ;
    }  
  
    public void OnLeave()  
    {
        //Debug.Log("离开idle状态");
        CurrPlayer.PlayerAnimator.SetBool("ToIdle", false);
    }  
  
    public void OnUpdate()  
    {
        //if (CurrPlayer.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        //{
        //    Debug.Log("执行idle状态");
        //    CurrPlayer.PlayerAnimator.SetInteger("CurrentState", GetStateID());
        //}
    }  
  
    public void OnFixedUpdate()  
    {  
    }  
  
    public void OnLateUpdate()  
    {  
    }  
 
    #endregion 
}
