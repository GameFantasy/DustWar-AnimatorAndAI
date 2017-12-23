using UnityEngine;
using System.Collections;

public class AnimationUpdate : MonoBehaviour
{
	public AnimationClip[] animations;
	
	Animator anim;
	
	public float delayWeight;
	
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	Vector2 scroll_pos;
	void OnGUI ()
	{
		int button_h = 25;
		
		
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		
		for(int i = 0 ;i<animations.Length;i++){
			if (GUILayout.Button (animations[i].name)) {
				anim.CrossFade (animations[i].name, 0);
			}
			
			if((i+1)%(Screen.height/button_h) == 0){
				GUILayout.EndHorizontal();
				GUILayout.BeginVertical();
			}
			
		}

		GUILayout.EndVertical();
	
	}
	
	float current = 0;
	
	
	void Update ()
	{
		
		if (Input.GetMouseButton (0)) {
			current = 1;
		} else {
			current = Mathf.Lerp (current, 0, delayWeight);
		}
	}
}
