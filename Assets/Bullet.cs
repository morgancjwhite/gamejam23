using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [NonSerialized] public GameObject target;
    private GameController gameController;


    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector3 bulletDirection = target.transform.position - transform.position;
        rb.velocity = bulletDirection.normalized * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Zombie"))
        {
            gameController.zombieCount--;
            // TODO spawn blood splatter 
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}