using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMatColor : MonoBehaviour
{
    private Material cubeMat;//使用代码动态创建

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            cubeMat = new Material(Shader.Find("Assets/NewMaterial1"));//在project窗口中找到这个shader脚本
        Debug.Log("材质球名称:"+cubeMat.name);

        float r = 100 / 255;
        float g = 255 / 255;
        float b = 150 / 255;
        cubeMat.color = new Color(r, g, b, 1);//设置颜色（注意：new Color 的rgb的数值范围是0-1 ，无论你给rgb 设置的什么都要除于255，这样才是你在unity里面看到的颜色。 color32则是 0-255的数值范围）

        gameObject.GetComponent<Renderer>().material = cubeMat;//将设置的颜色赋给你的物体
    }


}
