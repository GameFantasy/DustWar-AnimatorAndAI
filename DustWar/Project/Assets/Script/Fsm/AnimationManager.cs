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
    public void addAnimStates(string name,string actName,Animator m_animator)
    {
        AnimatorController animatorController = m_animator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine animStm = animatorController.layers[0].stateMachine;
        AnimationClip newClip = AssetDatabase.LoadAssetAtPath("Assets/wolf/Animations/wolf@Idle.FBX", typeof(AnimationClip)) as AnimationClip;
        AnimatorState animst = animStm.AddState(newClip.name);
        animst.motion = newClip;
    }

}
