using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    void Update() {
        
        //调用输入检测
        InputCheck();

    }

    /// <summary>
    /// 该控制器所有输入检测
    /// </summary>
    public void InputCheck()
    {
        InputCheck inputCheck = new InputCheck();
        inputCheck.Process(KeyCode.Mouse0, new InputCheck.InputProcess(KeyCheckDown_Mouse), Configure.InputType.Down);
        inputCheck.Process(KeyCode.Mouse0, new InputCheck.InputProcess(KeyCheck_Mouse), Configure.InputType.Always);
        inputCheck.Process(KeyCode.Mouse0, new InputCheck.InputProcess(KeyCheckUp_Mouse), Configure.InputType.Up);

    }

    #region 输入检测后执行的方法

    public void KeyCheckDown_Mouse()
    {
        Debug.Log("摁下鼠标左键");
    }
    public void KeyCheckUp_Mouse()
    {
        Debug.Log("抬起鼠标左键");
    }
    public void KeyCheck_Mouse()
    {
        Debug.Log("长摁鼠标左键");
    }

    #endregion



}
