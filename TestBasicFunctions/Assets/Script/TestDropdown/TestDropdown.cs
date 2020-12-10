using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 测试Dropdown组件例子脚本
/// </summary>
public class TestDropdown : MonoBehaviour
{
    void Start()
    {
        int testArg = 123;
        //代码动态绑定的方法，如果通过代码添加监听事件，外部就无需再做添加
        //GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener((int value)=> ConsoleResult(value,testArg));
    }


    /// <summary>
    /// 输出结果 —— 添加监听事件时要注意，需要绑定动态方法
    /// </summary>
    public void ConsoleResult(int value,int testArg)
    {
        //这里用 if else if也可，看自己喜欢
        //分别对应：第一项、第二项....以此类推
        switch (value)
        {
            case 0:
                print("第1页,测试传参:"+ testArg);

                break;
            case 1:
                print("第2页");
                break;
            case 2:
                print("第3页");
                break;
            case 3:
                print("第4页");
                break;
            //如果只设置的了4项，而代码中有第五个，是调用不到的
            //需要对应在 Dropdown组件中的 Options属性 中增加选择项即可
            case 4:
                print("第5页");
                break;
        }
    }
}