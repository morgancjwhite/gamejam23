using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // public GameObject opacity;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] public GameObject humanPrefab;
    [SerializeField] public GameObject policePrefab;
    [SerializeField] private int numHumans;
    [SerializeField] private int numPolice;
    private System.Random rnd;
    private int humanSpawnRangeHorizontal;
    private int humanSpawnRangeVertical;
    [NonSerialized] public int zombieCount;

    void Start()
    {
        rnd = new System.Random();
        humanSpawnRangeVertical = 8;
        humanSpawnRangeHorizontal = 15;
        zombieCount = 0;
        SpawnMobs();
    }

    void SpawnMobs()
    {
        GameObject startZombie = Instantiate(zombiePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        startZombie.GetComponent<ZombieMove>().speed = 1.5f;
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

    private void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            QuitGame();
        }

        if (zombieCount == 0)
        {
            QuitGame();
        }
    }
}