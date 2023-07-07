using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private float scale;
    private Rigidbody2D _rigidbody2D;
    private bool isBouncing;
    private System.Random rnd;
    private float speed;


    [SerializeField] private float speedLowerBound;
    [SerializeField] private float speedUpperBound;


    // Start is called before the first frame update
    void Start()
    {
        scale = 20f;
        isBouncing = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        rnd = new System.Random();
        speed = (float)rnd.Next((int)speedLowerBound, (int)speedUpperBound) / 100;
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

    // void OnCollisionEnter2D(Collision2D collision)
    // {
        // // get normal angle of collision, add some noise and send flying that way!
        // float bounce = bounceForce;
        // Vector2 normalAngle = collision.contacts[0].normal;
        // int randX = rnd.Next(-5, 5);
        // int randY = rnd.Next(-5, 5);
        // Vector2 bounceAngle = new Vector2(randX + normalAngle.x, randY + normalAngle.y);
        // _rigidbody2D.AddForce(bounceAngle * bounce);
        // isBouncing = true;
        // Invoke("StopBounce", boundEndTime);
    // }
    //
    // void StopBounce()
    // {
    //     isBouncing = false;
    // }
}