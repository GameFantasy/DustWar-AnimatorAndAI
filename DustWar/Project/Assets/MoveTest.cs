using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour 
{
    public float v =2.0f;//速度
    private Vector3 dir;//移动方向向量
    private Vector3 hitPos;//鼠标点击坐标
    private Animator anim;
    void Start() 
    {
        hitPos = transform.position;
        anim = GetComponent<Animator>();
    }
    void Update() 
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mhit;
            if (Physics.Raycast(ray, out mhit))
            {
                if (mhit.collider.gameObject.tag == "terrain")
                {
                    hitPos = mhit.point;
                }
            }
        }
        transform.LookAt(hitPos);
        dir = hitPos - transform.position;
        if (dir.magnitude > 0.05f)
        {
            dir.Normalize();
            anim.SetBool("isRun", true);
        }
        else 
        {
            dir = Vector3.zero;
            anim.SetBool("isRun", false);
            
        }
        transform.position += dir * v * Time.deltaTime;
    }
}
