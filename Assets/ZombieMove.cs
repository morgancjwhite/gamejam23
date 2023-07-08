using System;
using System.Collections;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private System.Random rnd;
    [NonSerialized] public float speed;
    private bool followingMouse;
    private GameController gameController;

    [SerializeField] private float conversionFollowDelayMilliseconds;
    [SerializeField] private float speedLowerBound;
    [SerializeField] private float speedUpperBound;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.zombieCount++;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        rnd = new System.Random();
        speed = (float)rnd.Next((int)speedLowerBound, (int)speedUpperBound) / 100;
        StartCoroutine(DelayMovement());
    }

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(conversionFollowDelayMilliseconds / 1000);
        followingMouse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (followingMouse && Input.GetMouseButton(0))
        {
            Vector3 moveDirection;
            moveDirection = Input.mousePosition;
            moveDirection.z = 0.0f;
            moveDirection = Camera.main.ScreenToWorldPoint(moveDirection);
            moveDirection -= transform.position;
            _rigidbody2D.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
    }
}