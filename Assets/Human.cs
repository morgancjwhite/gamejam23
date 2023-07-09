using System;
using System.Collections;
using UnityEngine;

public class Human : HumanBase
{
    [SerializeField] private float humanRunForce;
    [SerializeField] private int humanNoLongerScaredTime;
    [SerializeField] private int hitsUntilDeadHuman;
    [SerializeField] private Sprite woundedSprite;
    private System.Random rnd;
    private bool hasTriedSpeaking;

    new void Start()
    {
        hitsUntilDead = hitsUntilDeadHuman;
        woundedSprite1 = woundedSprite;
        woundedSprite2 = woundedSprite;
        rb = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
        rnd = new System.Random();
        hasTriedSpeaking = false;
    }

    void possibleVoiceLine()
    {
        if (rnd.Next(10) == 0)
        {
            _chatBubbleHandler.ShowText(transform.position, "humanScared");
        }
    }
    
    IEnumerator ResetSpeakingStatus()
    {
        yield return new WaitForSeconds(8f);
        hasTriedSpeaking = false;
    }

    protected override void ReactToZombie(GameObject zombie)
    {
        if (!hasTriedSpeaking)
        {
            possibleVoiceLine();
            StartCoroutine(ResetSpeakingStatus());
        }
        hasTriedSpeaking = true;
        RunAway(zombie);
        CancelInvoke();
        InvokeRepeating("WalkAround", humanNoLongerScaredTime, randomDirectionChangeTime);
    }


    public void RunAway(GameObject zombie)
    {
        Ray ray = new Ray(zombie.transform.position, transform.position - zombie.transform.position);
        rb.velocity = ray.direction * humanRunForce;
    }
}