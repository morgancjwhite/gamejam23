using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            Vector2 newPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
            transform.position = newPos;
        }
    }
}