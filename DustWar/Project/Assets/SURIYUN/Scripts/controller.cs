﻿//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class controller : MonoBehaviour
{
	
	public float animSpeed = 1.5f;				
	public float lookSmoother = 3.0f;			
	public bool useCurves = true;				
	public float useCurvesHeight = 0.5f;		
	public float forwardSpeed = 7.0f;
	public float backwardSpeed = 2.0f;
	public float rotateSpeed = 2.0f;
	public float jumpPower = 3.0f; 
	private CapsuleCollider col;
	private Rigidbody rb;
	private Vector3 velocity;
	private float orgColHight;
	private Vector3 orgVectColCenter;
	private Animator anim;						
	private AnimatorStateInfo currentBaseState;			
	private GameObject cameraObject;	
	
	
	//static int deadState = Animator.StringToHash("Base Layer.Dead");
	//static int ss_cState = Animator.StringToHash("Base Layer.SS_C");
	//static int dead0State = Animator.StringToHash("Base Layer.Dead0");
	//static int atkState = Animator.StringToHash("Base Layer.Atk");
	//static int atk2State = Animator.StringToHash("Base Layer.Atk2");
	//static int atk3State = Animator.StringToHash("Base Layer.Atk3");
	static int idle_cState = Animator.StringToHash("Base Layer.Idle_C");
	static int idle_aState = Animator.StringToHash("Base Layer.Idle_A");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int cute1State = Animator.StringToHash("Base Layer.Cute1");
	
	
	
	
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		cameraObject = GameObject.FindWithTag("MainCamera");
		orgColHight = col.height;
		orgVectColCenter = col.center;
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");				
		float v = Input.GetAxis("Vertical");				
		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h); 						
		anim.speed = animSpeed;								
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	
		rb.useGravity = true;
		
		
		
		velocity = new Vector3(0, 0, v);
		velocity = transform.TransformDirection(velocity);
		if (v > 0.1) {
			velocity *= forwardSpeed;	
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
		
		/*if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			anim.SetTrigger("Dead");
		}
		if(Input.GetKeyDown(KeyCode.Keypad5))
		{

			anim.SetBool("Atk",true);
		}*/
		
		if (Input.GetButtonDown("Jump")) {	
			if (currentBaseState.nameHash == locoState){
				if(!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}
		/*if (Input.GetKeyDown(KeyCode.Keypad7)) {
			if (currentBaseState.nameHash == locoState){
				if(!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("SS_C", true);		
				}
			}
		}*/
		
		
		transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate(0, h * rotateSpeed, 0);	
		if (currentBaseState.nameHash == locoState){
			if(useCurves){
				resetCollider();
			}
		}
		if(currentBaseState.nameHash == jumpState)
		{
			cameraObject.SendMessage("setCameraPositionJumpView");	
			if(!anim.IsInTransition(0))
			{
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{
							
							resetCollider();
						}
					}
				}
				anim.SetBool("Jump", false);
			}
		}
		
		/*else if(currentBaseState.nameHash == ss_cState)
		{
			cameraObject.SendMessage("setCameraPositionJumpView");
			if(!anim.IsInTransition(0))
			{
				
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{				
							resetCollider();
						}
					}
				}
				anim.SetBool("Jump_c", false);
			}
		}*/
		
		else if (currentBaseState.nameHash == idle_cState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}
			/*if(Input.GetKeyDown(KeyCode.Keypad5))
			{
				anim.SetTrigger("Atk");
			}*/
			
			
		}
		else if (currentBaseState.nameHash == idle_aState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}
			/*if(Input.GetKeyDown(KeyCode.Keypad5))
			{
				anim.SetTrigger("Atk");
			}*/
		}
		else if (currentBaseState.nameHash == cute1State)
		{
			
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Cute1", false);
			}
		}
		/*else if (currentBaseState.nameHash == atkState)
		{

			if(Input.GetKeyDown(KeyCode.Keypad6))
			{
				anim.SetBool("Atk2",true);
			}
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Atk", false);
			}

		}
		else if (currentBaseState.nameHash == atk2State)
		{
			if(Input.GetKeyDown(KeyCode.Keypad6))
			{
				anim.SetBool("Atk3",true);
			}
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Atk2", false);
			}
		}
		else if (currentBaseState.nameHash == atk3State)
		{
			if(Input.GetKeyDown(KeyCode.Keypad6))
			{
				anim.SetBool("Atk1",true);
			}
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Atk3", false);
			}
		}
		else if (currentBaseState.nameHash == deadState)
		{

		}*/
		
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width -260, 10 ,250 ,150), "Interaction");
		GUI.Label(new Rect(Screen.width -245,30,250,30),"Up/Down Arrow : Go Forwald/Go Back");
		GUI.Label(new Rect(Screen.width -245,50,250,30),"Left/Right Arrow : Turn Left/Turn Right");
		GUI.Label(new Rect(Screen.width -245,70,250,30),"Hit Space key while Running : Jump");
		GUI.Label(new Rect(Screen.width -245,90,250,30),"Hit Spase key while Stopping : Cute1");
		GUI.Label(new Rect(Screen.width -245,110,250,30),"Left Control : Front Camera");
		GUI.Label(new Rect(Screen.width -245,130,250,30),"Alt : LookAt Camera");
	}
	void resetCollider()
	{
		
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}
}
