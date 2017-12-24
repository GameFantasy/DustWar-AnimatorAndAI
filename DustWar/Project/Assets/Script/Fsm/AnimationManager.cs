using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager
{
    private static AnimationManager instance=null;
    public static AnimationManager Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance=new AnimationManager();
            }
            return instance;
        }
    }
    private Animator m_animator;
    public Animator animator
    {
        set{ m_animator=value;}
    }
    private AnimatorController animatorController;
    private AnimatorStateMachine animStM;
    /// <summary>
    /// 添加状态机状态
    /// </summary>
    /// <param name="actName">状态名</param>
    public void AddAnimState(string actName)
    {
         animatorController = m_animator.runtimeAnimatorController as AnimatorController;
         animStM = animatorController.layers[0].stateMachine;
         foreach (ChildAnimatorState chidstate in animStM.states) 
        {
            if (chidstate.state.name == actName) 
            {
                return;
            }
        }
         AnimatorState animSt = animStM.AddState(actName);
    }
    /// <summary>
    /// 删除状态机状态
    /// </summary>
    /// <param name="actName">状态名</param>
    public void DeleteAnimState(string actName)
    {
        animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        animStM = animatorController.layers[0].stateMachine;
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == actName)
            {
                animStM.RemoveState(chidstate.state);
                return;
            }
        }
        Debug.LogError("未找到状态:" + actName);
    }
    /// <summary>
    /// 添加或替换状态机状态的动画片段
    /// </summary>
    /// <param name="name">使用状态机角色名</param>
    /// <param name="stateName">状态名</param>
    /// <param name="m_animator">Animator组件</param>
    /// <param name="isFBX">是否FBX文件</param>
    public void AddAnimMoion(string name, string stateName,bool isFBX)
    {
         animatorController = m_animator.runtimeAnimatorController as AnimatorController;
         animStM = animatorController.layers[0].stateMachine;
        AnimationClip newClip;
        AnimatorState animState;
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == stateName)
            {
                if (isFBX)
                {
                    newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{0}@{1}.FBX", name, stateName), typeof(AnimationClip)) as AnimationClip;
                    animState = GetState(stateName);
                    animState.motion = newClip;
                    return;
                }
                else
                {
                    newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{1}.anim", name, stateName), typeof(AnimationClip)) as AnimationClip;
                    animState = GetState(stateName);
                    animState.motion = newClip;
                    return;
                }
            }
        }
        if (isFBX)
        {
            newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{0}@{1}.FBX", name, stateName), typeof(AnimationClip)) as AnimationClip;
            animatorController.AddMotion(newClip);
        }
        else
        {
            newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{1}.anim", name, stateName), typeof(AnimationClip)) as AnimationClip;
            animatorController.AddMotion(newClip);
        }
    }
    /// <summary>
    /// 增加转换条件
    /// </summary>
    /// <param name="state">状态名</param>
    /// <param name="transformSate">转换状态名</param>
    /// <param name="param">条件参数名</param>
    public void AddTransition(string state,string desState,string param) 
    {
        animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        animStM = animatorController.layers[0].stateMachine;
        AnimatorState animSt = GetState(state);
        if(animSt.transitions.Length!=0)
        foreach (AnimatorStateTransition animStTrans in animSt.transitions)
        {
            if (animStTrans.destinationState.name == GetState(desState).name)
            {
                Debug.LogError("此转换:" + state + " to " + desState + "已存在");
                return;
            }
        }
        AnimatorStateTransition trans = animSt.AddTransition(GetState(desState));
        animatorController.AddParameter(param, AnimatorControllerParameterType.Bool);
        trans.AddCondition(AnimatorConditionMode.If,0,param);
    }
    public void DeleteTransition(string state,string desState) 
    {
        AnimatorState animSt = GetState(state);
        foreach (AnimatorStateTransition animStTrans in animSt.transitions) 
        {
            if (animStTrans.destinationState.name == GetState(desState).name) 
            {
                animSt.RemoveTransition(animStTrans);
                return;
            }
        }
        Debug.LogError("未找到此转换"+state+" to "+desState);
    }
    private AnimatorState GetState(string state)
    {
        foreach (ChildAnimatorState chidstate in animStM.states)
        {
            if (chidstate.state.name == state)
            {
                return chidstate.state;
            }
        }
        Debug.LogError("未找到状态:" + state);
        return null;
    }

}
