using System;
using System.Collections;
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
    public GameObject nuke;
    [SerializeField] private int mapSize; // this is a guess, for spawning mobs
    public int timeToNuke;

    void Start()
    {
        rnd = new System.Random();
        humanSpawnRangeVertical = 8;
        humanSpawnRangeHorizontal = 15;
        zombieCount = 0;
        SpawnEntities();
        nuke.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator WaitForMobToLoad(GameObject mob)
    {
        yield return new WaitUntil(() => mob.GetComponent<HumanBase>().updating);
        if (mob.GetComponent<HumanBase>().spawnCollide)
        {
            Destroy(mob);
        }
    }

    void SpawnHumanBaseMobs(GameObject prefab, int numMobs)
    {
        for (int i = 1; i < numMobs; i++)
        {
            // TODO want mobs to spawn outside of human spawn range
            int rndX = rnd.Next(-mapSize, mapSize);
            int rndY = rnd.Next(-mapSize, mapSize);
            GameObject mob = Instantiate(prefab, new Vector3(rndX, rndY, -1), Quaternion.identity);
            StartCoroutine(WaitForMobToLoad(mob));
        }
    }

    private void SpawnCycleHumans()
    {
        // every 4 seconds
        SpawnHumanBaseMobs(humanPrefab, 3);
    }

    private void SpawnCyclePolice()
    {
        // every 6 seconds
        SpawnHumanBaseMobs(policePrefab, 2);
    }

    void SpawnEntities()
    {
        GameObject startZombie = Instantiate(zombiePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        startZombie.GetComponent<ZombieMove>().speed = 1.5f;
        SpawnHumanBaseMobs(humanPrefab, numHumans);
        SpawnHumanBaseMobs(policePrefab, numPolice);
        InvokeRepeating("SpawnCycleHumans", 3f, 4f);
        InvokeRepeating("SpawnCyclePolice", 3f, 6f);
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

    IEnumerator DelayQuit(float seconds)
    {
        print("start delay, see nuke?");
        yield return new WaitForSeconds(seconds);
        print("delay finished");
        QuitGame();
    }

    public void Nuke()
    {
        print("nuke called");
        nuke.GetComponent<SpriteRenderer>().enabled = true;
        GameObject camera = GameObject.Find("Main Camera");
        nuke.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -8);
        StartCoroutine(DelayQuit(3f));
    }
}