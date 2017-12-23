using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour {

    /// <summary>
    /// 委托类型，输入检测后执行的方法
    /// </summary>
    public delegate void InputProcess();
    
    /// <summary>
    /// 键鼠输入检测方法
    /// </summary>
    /// <param name="keyCode">点击的按键</param>
    /// <param name="process">执行的方法</param>
    /// <param name="inputType">输入的类型(长摁，点下，抬起),默认值为点下</param>
    public void Process(KeyCode keyCode, InputProcess process,Configure.InputType inputType= Configure.InputType.Down)
    {
        //通过不同输入方式执行操作
        switch (inputType)
        {
            //长摁
            case Configure.InputType.Always:
                if (Input.GetKey(keyCode))
                {
                    process();
                }
                break;
            //抬起
            case Configure.InputType.Up:
                if (Input.GetKeyUp(keyCode))
                {
                    process();
                }
                break;
            //点下
            case Configure.InputType.Down:
                if (Input.GetKeyDown(keyCode))
                {
                    process();
                }
                break;
        }
    }
          
}
