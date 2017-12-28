using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    GameStateMachine CurrStateMachine = new GameStateMachine();
    public enum PlayerState 
    {
        idle=0,
        run,
        attack,
        skillQ,
        skillW
    }
    [SerializeField]
    private float Velocity = 0.5f; //角色移动速度
    [SerializeField]
    private float PlayerRotateVelocity = 0.5f; //角色转身速度
    //角色运动方向
    private Vector3 Direction;
    //鼠标点击坐标
    private Vector3 hitPos;
    //动画组件
    public Animator PlayerAnimator{get;private set;}
    //public AnimatorStateInfo CurrPlayerAnimatorStateInfo{ get; set; }
    void Awake() 
    {
        PlayerAnimator = GetComponent<Animator>();
        //注册状态
        CurrStateMachine.RegistState(new PlayerIdleState(this));
        CurrStateMachine.RegistState(new PlayerRunState(this));
        CurrStateMachine.SwitchState((int)PlayerState.idle, null, null);

    }
    void Start() 
    {
        hitPos = transform.position;
    }
    void Update() 
    {
        if (CurrStateMachine != null)
        {
            CurrStateMachine.OnUpdate();
        }
        hitPos = GetMouseHitposition(hitPos);
        PlayerRotate(hitPos,PlayerRotateVelocity);
        Direction = (hitPos - transform.position).normalized;
        if (Vector3.Distance(hitPos, transform.position) < 0.1f)
        {
            CurrStateMachine.SwitchState((int)PlayerState.idle, null, null);
            Direction = Vector3.zero;
            transform.Translate(Direction * Velocity * Time.deltaTime,Space.World);
            
        }
        else
        {
            CurrStateMachine.SwitchState((int)PlayerState.run, null, null);
            transform.Translate(Direction * Velocity * Time.deltaTime, Space.World);
        }
    }
    #region 获得鼠标点击坐标方法
    /// <summary>
    /// 获得鼠标点击坐标方法
    /// </summary>
    /// <param name="hitPos">点击坐标</param>
    Vector3 GetMouseHitposition(Vector3 hitPos)
    {
        if (Input.GetMouseButtonDown(1))
        {
            int layerMask = 1 << 10;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mhit;
            if (Physics.Raycast(ray, out mhit, 1000f, layerMask))
            {
                hitPos = mhit.point;
            }
        }
        return hitPos;
    }
    #endregion
    #region 角色转身方法
    /// <summary>
    /// 角色转身方法
    /// </summary>
    /// <param name="hitPos">点击坐标</param>
    /// <param name="RotateVelocity">转身速度（秒）</param>
    void PlayerRotate(Vector3 hitPos,float RotateVelocity) 
    {
        Quaternion playerRotation = Quaternion.LookRotation(hitPos - transform.position);
        transform.rotation=Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * (5.0f/RotateVelocity));
    }
    #endregion

}
