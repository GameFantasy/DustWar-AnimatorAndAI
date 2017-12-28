using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    public Player CurrPlayer { get; private set; }
    public PlayerRunState(Player currPlayer)  
    {
        CurrPlayer = currPlayer;
    }  
    #region IState 成员  
    public int GetStateID()  
    {  
        return (int)Player.PlayerState.run;  
    }  
  
    public void OnEnter()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToRun", true);
    }  
  
    public void OnLeave()  
    {
        CurrPlayer.PlayerAnimator.SetBool("ToRun", false);
    }  
  
    public void OnUpdate()  
    {
        //Debug.Log("执行run状态");

        //if (CurrPlayer.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        //{
        //    Debug.Log("执行run状态");
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
