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
    [SerializeField] private float humanRunSpeed;
    private bool scaredHuman;


    // Start is called before the first frame update
    void Start()
    {
        scaredHuman = false;
    }

    //
    // Update is called once per frame
    void Update()
    {
        if (!scaredHuman)
        {
            CheckZombiesInRadius();
        }
    }

    void CheckZombiesInRadius()
    {
        bool zombieClose = false;
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("zombie"))
        {
            if (Vector3.Distance(zombie.transform.transform.position, gameObject.transform.position) < humanScanRadius)
            {
                RunAway(zombie);
                scaredHuman = true;
            }
        }
    }

    public void RunAway(GameObject zombie)
    {
        print("run away");
        print(zombie.transform.position);
        float angle = Vector3.Angle(zombie.transform.position, transform.position);
        Ray ray = new Ray(zombie.transform.position, transform.position - zombie.transform.position);
        gameObject.GetComponent<Rigidbody2D>().AddForce(ray.direction * humanRunSpeed);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.Contains("Zombie"))
        {
            print("inside collision");
            GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.Euler(0, 0, 0));
            float bounce = bounceForce;
            Vector2 normalAngle = collision.contacts[0].normal;
            Vector2 bounceAngle = normalAngle;
            zombie.GetComponent<Rigidbody2D>().AddForce(bounceAngle * bounce);
            Destroy(gameObject);
        }
    }
}