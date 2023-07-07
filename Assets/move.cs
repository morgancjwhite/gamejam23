using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float scale;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        scale = 20f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(1 / scale, 2 / scale, 3 / scale);

        Vector3 moveDirection;
        moveDirection = Input.mousePosition;
        moveDirection.z = 0.0f;
        moveDirection = Camera.main.ScreenToWorldPoint(moveDirection);
        moveDirection -= transform.position;
        _rigidbody2D.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        
    }
}