using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("trying");
        if (Physics.CheckSphere(transform.position, 3f))
        {
            print("succeeding");

        }
        else
        {
            print("failing");
        }
    }
}
