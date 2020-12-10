using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childCountTest: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Example();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

        void Example()
        {
            print(transform.childCount);
        }

}
