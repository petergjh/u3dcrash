using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoubleToFloat : MonoBehaviour
{
    void Start()
    {
        float fm = (float)3388608m;
        Debug.Log("强转结果fm = "+fm);

        float fd = (float)3388608d;
        Debug.Log("强转结果fd = "+fd);
  
        float f1m = (float)0.0001m;
        Debug.Log("强转结果f1m = "+f1m);

        float f1d = (float)0.0001d;
        Debug.Log("强转结果f1d = "+f1d);

        float f3m = (float)0.123456789m;
        Debug.Log("强转结果f3m = "+f3m);

        float f3d = (float)0.123456789d;
        Debug.Log("强转结果f3d = "+f3d);

    }

}
