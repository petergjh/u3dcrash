using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldableMenu : MonoBehaviour
{
    private RectTransform content;//父物体的parent
    private TextAsset textAsset;//所有菜单信息
    private RectTransform parentRect;//父菜单的prefab
    private RectTransform[] parentArr;//所有父菜单的数组
    private RectTransform childRect;//子菜单的prefab
    private Vector3 parentOffset;//单个父菜单的高度
    private Vector3 childOffset;//单个父菜单的高度
    private int[] cntArr;//所有父菜单拥有的子菜单个数

    void Awake()
    {
        Init();
    }

    void Init()
    {
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        textAsset = Resources.Load<TextAsset>("menuInfo");

        parentRect = Resources.Load<RectTransform>("parentMenu");
        parentOffset = new Vector3(0, parentRect.rect.height);

        childRect = Resources.Load<RectTransform>("item");
        childOffset = new Vector3(0, childRect.rect.height);

        var info = textAsset.text.Split(',');//获取子菜单个数信息
        cntArr = new int[info.Length];
        parentArr = new RectTransform[info.Length];
        //初始化content高度
        content.sizeDelta = new Vector2(content.rect.width, parentArr.Length * parentRect.rect.height);

        for (int i = 0; i < cntArr.Length; i++)
        {
            parentArr[i] = Instantiate(parentRect, content.transform);
            parentArr[i].localPosition -= i * parentOffset;
            cntArr[i] = int.Parse(info[i]);
            parentArr[i].GetComponent<ParentMenu>().Init(childRect, cntArr[i]);
            int j = i;
            parentArr[i].GetComponent<Button>().onClick.AddListener(() => { OnButtonClick(j); });
        }
    }

    void OnButtonClick(int i)
    {
        if (!parentArr[i].GetComponent<ParentMenu>().isCanClick) return;
        parentArr[i].GetComponent<ParentMenu>().isCanClick = false;
        if (!parentArr[i].GetComponent<ParentMenu>().isOpening)
            StartCoroutine(MenuDown(i));
        else
            StartCoroutine(MenuUp(i));
    }

    IEnumerator MenuDown(int index)
    {       
        for (int i = 0; i < cntArr[index]; i++)
        {
            //更新content高度
            content.sizeDelta = new Vector2(content.rect.width,
                content.rect.height + childOffset.y);
            for (int j = index + 1; j < parentArr.Length; j++)
            {
                parentArr[j].localPosition -= childOffset;
            }
            yield return new WaitForSeconds(0.1f);
        }     
    }

    IEnumerator MenuUp(int index)
    {
        for (int i = 0; i < cntArr[index]; i++)
        {
            //更新content高度
            content.sizeDelta = new Vector2(content.rect.width,
                content.rect.height - childOffset.y);
            for (int j = index + 1; j < parentArr.Length; j++)
            {
                parentArr[j].localPosition += childOffset;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
