using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
        /// <summary>  
        /// 获取状态ID  
        /// </summary>  
        /// <returns></returns>  
        int GetStateID();

        /// <summary>  
        /// 进入状态回调此方法  
        /// </summary>  
        /// <param name="stateMachine">控制此状态的状态机</param>  
        /// <param name="prevState">进入此状态的前状态</param>  
        void OnEnter();

        /// <summary>  
        /// 离开状态时回调此方法  
        /// </summary>  
        /// <param name="nextState">离开此状态后的下一状态</param>  
        void OnLeave();

        /// <summary>  
        /// 每帧的OnUpdate方法回调  
        /// </summary>  
        void OnUpdate();

        /// <summary>  
        /// 每帧的FixedUpdate回调  
        /// </summary>  
        void OnFixedUpdate();

        /// <summary>  
        /// 每帧的LateUpdate回调  
        /// </summary>  
        void OnLateUpdate(); 
}
