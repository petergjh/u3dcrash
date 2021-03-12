using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSerialize : MonoBehaviour
{
    public InputField uiInputTxt;
    public Text uiTxtOutput;
    public Button uiBtnSave;
    public Button uiBtnShow;
    private TestSaveData _testDataObj;


    // Start is called before the first frame update
    void Start()
    {
        _testDataObj = new TestSaveData();
        Debug.Log("实例化一个数据类对象,用来存放具体数据");
        
        uiInputTxt.onValueChanged.AddListener(GetInputTxt);
        
        uiBtnSave.onClick.AddListener(
            () =>
            {
                BinaryTool.SaveBinaryData<TestSaveData>(_testDataObj, "DataFile");
                Debug.Log("序列化对象, _testDataObj, 内容为："+ _testDataObj.inputStr);
                
            });
        
        uiBtnShow.onClick.AddListener(
            () =>
            {
               _testDataObj = BinaryTool.ReadBinaryData<TestSaveData>("DataFile");
                Debug.Log("反序列化对象, _testDataObj, 读取内容为："+ _testDataObj.inputStr);
               uiTxtOutput.text = _testDataObj.inputStr;
            });
    }

    private void GetInputTxt(string arg0)
    {
        _testDataObj.inputStr = uiInputTxt.text;
        Debug.Log("开始输入字符，并把这些字符赋值给那个数据类实例对象");
    }
}
