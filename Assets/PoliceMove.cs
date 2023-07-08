using UnityEngine;

public class PoliceMove : HumanBase
{
    [SerializeField] private float initialShootDelay;
    [SerializeField] private float repeatingShootDelay;
    private GameObject zombieShootingAt;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int hitsUntilDeadPolice;
    [SerializeField] private Sprite woundedSprite1Police;
    [SerializeField] private Sprite woundedSprite2Police;
    private bool foundZombie;


    private new void Start()
    {
        hitsUntilDead = hitsUntilDeadPolice;
        woundedSprite1 = woundedSprite1Police;
        woundedSprite2 = woundedSprite2Police;
        rb = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
        foundZombie = false;
    }

    protected override void ReactToZombie(GameObject zombie)
    {
        zombieShootingAt = zombie;
        if (!foundZombie)
        {
            CancelInvoke(nameof(WalkAround));
            InvokeRepeating(nameof(ShootZombie), initialShootDelay, repeatingShootDelay);
            foundZombie = true;
            // add time after which it stops shooting?
        }
        // if zombie dies then?
    }

    void ShootZombie()
    {
        if (zombieShootingAt != null)
        {
            Vector3 bulletStartPosition = Vector3.Lerp(transform.position, zombieShootingAt.transform.position, 0.2f);
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = zombieShootingAt;
        }
    }
}