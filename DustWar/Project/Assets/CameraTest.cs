using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour
{

    //目标物体
    public GameObject target;
    //镜头高度
    public float maxHight = 22f;
    public float minHight = 5f;
    //镜头移动速度
    public float moveSpeed = 0.2f;
    //镜头移动距离
    private float moveDistanc;
    //距离变化速度
    public float zoomSpeed = 0.2f;

    void Start()
    {

    }

    void LateUpdate() 
    {
        //一些判断
        if (target == null)
            return;
        if (Camera.main == null)
            return;
        //目标的坐标
        Vector3 targetPos = target.transform.position;
        MoveCamera();
        if (Input.GetKey(KeyCode.Space)) 
        {
            BackView();
        }
    }
    //控制移动视角
    void MoveCamera() 
    {
        float v, h,height=0f;
        Vector3 moveVector;
        v = Input.GetAxis("Vertical")*moveSpeed;
        h = Input.GetAxis("Horizontal")*moveSpeed;
        height = Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;
        if (Input.mousePosition.x >= Screen.width) 
        {
            h += moveSpeed;
        } 
        if (Input.mousePosition.x <= 0)
        {
            h -= moveSpeed;
        }
        if (Input.mousePosition.y >= Screen.height)
        {
            v += moveSpeed;
        } 
        if (Input.mousePosition.y <= 0)
        {
            v -= moveSpeed;
        }
        moveVector.x = h;
        moveVector.y = -height;
        moveVector.z = v;
        transform.Translate(moveVector, Space.World);
    }
    //返回视角
    void BackView() 
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width*0.5f,Screen.height*0.5f,0f));
        if (Physics.Raycast(ray, out hit)) 
        {
            Vector3 backVector= target.transform.position-hit.point;
            backVector.y=0;
            transform.position += backVector;
        }
    }

}