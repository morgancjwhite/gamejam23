using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blood : MonoBehaviour
{
    [SerializeField] private float bloodSpeed;
    [SerializeField] private float dieTime;
    private Vector3 forwards = new Vector3(0, 1, 0);
    private bool stop = false;

    // private System.Random rnd;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        float randomZAngle = Random.Range(0, 360);
        StartCoroutine(DelayDie());
        transform.rotation = Quaternion.Euler(0, 0, randomZAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.Translate(bloodSpeed * forwards);
        }
    }

    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(Random.Range(0, dieTime));
        // Destroy(gameObject);
        stop = true;
    }
}