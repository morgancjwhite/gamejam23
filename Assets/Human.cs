using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class Human : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float bounceForce;
    [SerializeField] private float humanScanRadius;
    [SerializeField] private float humanRunForce;
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float humanWalkForce;
    [SerializeField] private int randomDirectionChangeTime;
    [SerializeField] private int humanNoLongerScaredTime;
    [SerializeField] private int hitsUntilDead;
    [SerializeField] private Sprite woundedSprite;


    private bool scaredHuman;
    private List<Vector2> cardinalDirections;
    private System.Random rnd;
    private int timeHitByZombie;
    private bool invincible;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        scaredHuman = false;
        rnd = new System.Random();

        cardinalDirections = new List<Vector2>();
        cardinalDirections.Add(new Vector2(1, 0));
        cardinalDirections.Add(new Vector2(0, 1));
        cardinalDirections.Add(new Vector2(-1, 0));
        cardinalDirections.Add(new Vector2(0, -1));
        int repeatTime = rnd.Next(2 * 1000, randomDirectionChangeTime * 1000);
        InvokeRepeating("WalkAround", 0f, (float)repeatTime / 1000);
        timeHitByZombie = 0;
        invincible = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckZombiesInRadius();
        rb.velocity = new Vector2(Math.Min(rb.velocity.x, maxRunSpeed), Math.Min(rb.velocity.y, maxRunSpeed));
    }

    void WalkAround()
    {
        rb.velocity = new Vector3(0, 0, 0);
        int index = rnd.Next(cardinalDirections.Count);
        Vector2 walkDirection = cardinalDirections[index];
        rb.AddForce(walkDirection * humanWalkForce);
    }


    void ReactToZombie(GameObject zombie)
    {
        RunAway(zombie);
        CancelInvoke();
        InvokeRepeating("WalkAround", humanNoLongerScaredTime, randomDirectionChangeTime);
    }
    
    void CheckZombiesInRadius()
    {
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("zombie"))
        {
            if (Vector3.Distance(zombie.transform.transform.position, gameObject.transform.position) < humanScanRadius)
            {
                ReactToZombie(zombie);
                break;
            }
        }
    }

    IEnumerator ResetInvincibleStatus()
    {
        yield return new WaitForSeconds(0.4f);
        invincible = false;
    }

    public void RunAway(GameObject zombie)
    {
        Ray ray = new Ray(zombie.transform.position, transform.position - zombie.transform.position);
        rb.AddForce(ray.direction * humanRunForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible && collision.collider.gameObject.name.Contains("Zombie"))
        {
            timeHitByZombie++;
            invincible = true;
            StartCoroutine(ResetInvincibleStatus());
            Vector2 normalAngle = collision.contacts[0].normal;
            rb.AddForce(normalAngle * bounceForce);
            gameObject.GetComponent<SpriteRenderer>().sprite = woundedSprite;
            if (timeHitByZombie >= hitsUntilDead)
            {
                Instantiate(zombiePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}