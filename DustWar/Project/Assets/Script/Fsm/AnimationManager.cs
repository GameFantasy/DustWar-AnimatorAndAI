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
    /// <summary>
    /// 添加状态机状态
    /// </summary>
    /// <param name="actName">状态名</param>
    /// <param name="m_animator">Anmator组件</param>
    public void AddAnimState(string actName,Animator m_animator)
    {
        AnimatorController animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine animStm = animatorController.layers[0].stateMachine;
        foreach (ChildAnimatorState chidstate in animStm.states) 
        {
            if (chidstate.state.name == actName) 
            {
                return;
            }
        }
        AnimatorState animst = animStm.AddState(actName);
    }
    /// <summary>
    /// 添加或替换状态机状态的动画片段
    /// </summary>
    /// <param name="name">使用状态机角色名</param>
    /// <param name="stateName">状态名</param>
    /// <param name="m_animator">Animator组件</param>
    /// <param name="isFBX">是否FBX文件</param>
    public void AddAnimMoion(string name, string stateName,  Animator m_animator,bool isFBX)
    {
        AnimatorController animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine animStm = animatorController.layers[0].stateMachine;
        AnimationClip newClip;
        AnimatorState animState;
        foreach (ChildAnimatorState chidstate in animStm.states)
        {
            if (chidstate.state.name == stateName)
            {
                if (isFBX)
                {
                    newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{0}@{1}.FBX", name, stateName), typeof(AnimationClip)) as AnimationClip;
                    animState = GetState(stateName, animStm);
                    animState.motion = newClip;
                    return;
                }
                else
                {
                    newClip = AssetDatabase.LoadAssetAtPath(string.Format(@"Assets/{0}/Animations/{1}.anim", name, stateName), typeof(AnimationClip)) as AnimationClip;
                    animState = GetState(stateName, animStm);
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
    public void AddTrisition(Animator m_animator, string state,string transformSate) 
    {
        AnimatorController animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine animStm = animatorController.layers[0].stateMachine;
        AnimatorState anist = GetState(state, animStm);
        anist.AddTransition(GetState(transformSate, animStm));
    }
    private AnimatorState GetState(string name, AnimatorStateMachine m_animStm)
    {
        foreach (ChildAnimatorState chidstate in m_animStm.states)
        {
            if (chidstate.state.name == name)
            {
                return chidstate.state;
            }
        }
        Debug.LogError("未找到状态:"+name);
        return null;
    }

}
