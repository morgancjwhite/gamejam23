using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float bounceForce;
    public float humanScanRadius;
    [NonSerialized] public float humanWalkForce;
    public int randomDirectionChangeTime;
    public int maxRunSpeed;
    [NonSerialized] public int hitsUntilDead;
    [NonSerialized] public Sprite woundedSprite1;
    [NonSerialized] public Sprite woundedSprite2;


    private List<Vector2> cardinalDirections;
    private System.Random rnd;
    private int timeHitByZombie;
    private bool invincible;
    [NonSerialized] public Rigidbody2D rb;


    // Start is called before the first frame update
    public void Start()
    {
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
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckZombiesInRadius();
        if(rb.velocity.sqrMagnitude > maxRunSpeed)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            rb.velocity *= 0.85f;
        }
    }

    void WalkAround()
    {
        rb.velocity = new Vector3(0, 0, 0);
        int index = rnd.Next(cardinalDirections.Count);
        Vector2 walkDirection = cardinalDirections[index];
        rb.AddForce(walkDirection * humanWalkForce);
    }

    protected virtual void ReactToZombie(GameObject zombie)
    {
        //to be overriden
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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible && collision.collider.gameObject.name.Contains("Zombie"))
        {
            timeHitByZombie++;
            invincible = true;
            StartCoroutine(ResetInvincibleStatus());
            Vector2 normalAngle = collision.contacts[0].normal;
            rb.AddForce(normalAngle * bounceForce);
            if (timeHitByZombie == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = woundedSprite1;
            }

            if (timeHitByZombie == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = woundedSprite2;
            }

            if (timeHitByZombie >= hitsUntilDead)
            {
                Instantiate(zombiePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}