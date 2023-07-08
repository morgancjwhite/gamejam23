using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [System.NonSerialized] public string state;

    // public GameObject opacity;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] public GameObject humanPrefab;
    [SerializeField] public GameObject policePrefab;
    [SerializeField] private int numHumans;
    [SerializeField] private int numPolice;
    private System.Random rnd;
    private int humanSpawnRangeHorizontal;
    private int humanSpawnRangeVertical;

    void Start()
    {
        state = "start";
        rnd = new System.Random();
        humanSpawnRangeVertical = 8;
        humanSpawnRangeHorizontal = 15;

        SpawnMobs();
    }

    void SpawnMobs()
    {
        Instantiate(zombiePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        for (int i = 1; i <= numHumans; i++)
        {
            int rndX = rnd.Next(-humanSpawnRangeHorizontal, humanSpawnRangeHorizontal);
            int rndY = rnd.Next(-humanSpawnRangeVertical, humanSpawnRangeVertical);
            Instantiate(humanPrefab, new Vector3(rndX, rndY, -1), Quaternion.identity);
        }
        for (int i = 1; i <= numPolice; i++)
        {
            int rndX = rnd.Next(-humanSpawnRangeHorizontal, humanSpawnRangeHorizontal);
            int rndY = rnd.Next(-humanSpawnRangeVertical, humanSpawnRangeVertical);
            Instantiate(policePrefab, new Vector3(rndX, rndY, -1), Quaternion.identity);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
}