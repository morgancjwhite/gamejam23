using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class Human : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float bounceForce;
    [SerializeField] private float humanScanRadius;


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        CheckZombiesInRadius();
    }

    void CheckZombiesInRadius()
    {
        // Collider[] colliders = Physics.OverlapSphere(transform.position, humanScanRadius);
        print(transform.position);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20000000f);
        if (hitColliders.Length != 0) {print(hitColliders);};
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("AddDamage");
        }
        // Vector2 avgPos;
        // if (colliders.Length > 0)
        // {
        //     var totalX = 0f;
        //     var totalY = 0f;
        //     foreach (Collider zombie in colliders)
        //     {
        //         totalX += zombie.transform.position.x;
        //         totalY += zombie.transform.position.y;
        //     }
        //
        //     var numZombies = Enumerable.Count(colliders);
        //     print(numZombies);
        //     
        //     avgPos = new Vector2(totalX / numZombies, totalY / numZombies);
        //     print(avgPos);
        //     MoveZombieAway(avgPos);
        // }
    }

    void MoveZombieAway(Vector2 avgPos)
    {
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.Contains("Zombie"))
        {
            GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.Euler(0, 0, 0));
            float bounce = bounceForce;
            Vector2 normalAngle = collision.contacts[0].normal;
            Vector2 bounceAngle = normalAngle;
            zombie.GetComponent<Rigidbody2D>().AddForce(bounceAngle * bounce);
            Destroy(gameObject);
        }
    }
}