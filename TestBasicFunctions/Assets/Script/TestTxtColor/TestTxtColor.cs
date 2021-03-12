using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTxtColor : MonoBehaviour
{
    public Text textObj;
    public Text textObj2;
    // Start is called before the first frame update
    void Start()
    {
       textObj = SetFontColorSize(textObj, "#696B32", 20);
       textObj2 = SetFontColorSize(textObj2, "#00f90e", 20);
    }
    
    /// <summary>
    /// 设置文本字体颜色和大小
    /// </summary>
    /// <param name="skillTipText"></param>
    /// <param name="yellow"></param>
    /// <param name="i"></param>
    /// <exception cref="NotImplementedException"></exception>
    private Text SetFontColorSize(Text textObj, string colorStr, int fontSize)
    {
        Color tempColor;
        ColorUtility.TryParseHtmlString(colorStr, out tempColor);
        textObj.color = tempColor;
        textObj.GetComponent<Text>().fontSize = fontSize;
        return textObj;
    }
}
