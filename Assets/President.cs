using System.Collections;
using UnityEngine;

public class President : HumanBase
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
        CancelInvoke("WalkAround");
    }

    protected override void ReactToZombie(GameObject zombie)
    {
        zombieShootingAt = zombie;
        if (!foundZombie)
        {
            _chatBubbleHandler.ShowText(transform.position, "enemy");
            humanWalkForce *= 2;
            randomDirectionChangeTime /= 2;
            InvokeRepeating(nameof(ShootZombie), initialShootDelay, repeatingShootDelay);
            foundZombie = true;
            StartCoroutine(ResetFoundZombieStatus());
            // add time after which it stops shooting?
        }
        // if zombie dies then?
    }

    IEnumerator ResetFoundZombieStatus()
    {
        yield return new WaitForSeconds(3f);
        foundZombie = false;
        CancelInvoke(nameof(ShootZombie));
        CancelInvoke("WalkAround");
    }

    void ShootZombie()
    {
        if (zombieShootingAt != null)
        {
            float lerpScale = 1f / Vector3.Distance(transform.position, zombieShootingAt.transform.position);
            Vector3 bulletStartPosition =
                Vector3.Lerp(transform.position, zombieShootingAt.transform.position, lerpScale);
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = zombieShootingAt;
            bullet.GetComponent<Bullet>().originPolice = gameObject;
        }
    }
}