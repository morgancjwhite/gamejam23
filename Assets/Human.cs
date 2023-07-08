using UnityEngine;

public class Human : HumanBase
{
    [SerializeField] private float humanRunForce;
    [SerializeField] private int humanNoLongerScaredTime;
    [SerializeField] private int hitsUntilDeadHuman;
    [SerializeField] private Sprite woundedSprite;

    void Start()
    {
        hitsUntilDead = hitsUntilDeadHuman;
        woundedSprite1 = woundedSprite;
        woundedSprite2 = woundedSprite;
        rb = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
    }

    protected override void ReactToZombie(GameObject zombie)
    {
        RunAway(zombie);
        CancelInvoke();
        InvokeRepeating("WalkAround", humanNoLongerScaredTime, base.randomDirectionChangeTime);
    }


    public void RunAway(GameObject zombie)
    {
        Ray ray = new Ray(zombie.transform.position, transform.position - zombie.transform.position);
        rb.velocity =  ray.direction * humanRunForce;
    }
}