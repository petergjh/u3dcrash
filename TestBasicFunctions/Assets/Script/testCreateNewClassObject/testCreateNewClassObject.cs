using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCreateNewClassObject : MonoBehaviour
{

    string cell;
    List<string> testList_1 = new List<string>() {"one","Two","Three" }; 
    List<string> cellList = new List<string>();

    void Start()
    {
        Test1();
        
    }

    private void Test1()
    {
        for (int i = 0; i < testList_1.Count; i++)
        {
            cell = testList_1[i];
            cellList.Add(cell);
        }
        foreach (string testCell in cellList)
        {
            print("testCell="+testCell);
        }

    }

}
