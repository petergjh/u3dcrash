using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTest2 : MonoBehaviour
{

    public Dropdown dropDown;

    void Start()
    {

        //是否可以点击
        dropDown.interactable = true;

        #region 添加下拉选项，，，设置文字，底图
        //添加一个下拉选项
        Dropdown.OptionData data = new Dropdown.OptionData();
        data.text = "方案一";
        //data.image = "指定一个图片做背景不指定则使用默认"；
        dropDown.options.Add(data);

        //另一种添加方式 , 不过用起来并不比第一个方便，
        List<Dropdown.OptionData> listOptions = new List<Dropdown.OptionData>();
        listOptions.Add(new Dropdown.OptionData("方案二"));
        listOptions.Add(new Dropdown.OptionData("方案三"));
        dropDown.AddOptions(listOptions);

        //设置显示字体大小
        dropDown.captionText.fontSize = 14;
        //dropDown.captionImage = "底图";
        //设置要复制字体大小
        dropDown.itemText.fontSize = 15;
        //dropDown.itemImage = "底图";

        //PS：我一般是使用循环 使用第一种形式添加
        #endregion

        //#region 添加完成就可以使用了，那么当我们想要复用怎么办呢？，这时就用到了移除OptionData，下面的每个注释打开都是一个功能
        ////直接清理掉所有的下拉选项，
        //dropDown.ClearOptions();
        ////亲测不是很好用
        ////dropDown.options.Clear(); 

        ////对象池回收时，有下拉状态的，直接干掉... (在极限点击测试的情况下会出现)
        //if (dropDown.transform.childCount == 3)
        //{
        //    Destroy(dropDown.transform.GetChild(2).gameObject);
        //}

        ////移除指定数据   参数：OptionData
        //dropDown.options.Remove(data);
        ////移除指定位置   参数:索引
        //dropDown.options.RemoveAt(0);
        //#endregion

        #region 添加监听函数
        //当点击后值改变是触发 (切换下拉选项)
        dropDown.onValueChanged.AddListener((int v) => OnValueChange(v));
        //若有多个，可以将自己当做参数传递进去，已做区分。
        //dropDown.onValueChanged_1.AddListener((int v) => OnValueChange(dropDown.gameobject,v));
        #endregion
    }

    /// <summary>
    /// 当点击后值改变是触发 (切换下拉选项)
    /// </summary>
    /// <param name="v">是点击的选项在OptionData下的索引值</param>
    void OnValueChange(int v)
    {
        //切换选项 时处理其他的逻辑...
        Debug.Log("点击下拉控件的索引是..." + v);
    }

}
