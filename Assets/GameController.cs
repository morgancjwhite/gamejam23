using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // public GameObject opacity;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] public GameObject humanPrefab;
    [SerializeField] public GameObject prezPrefab;
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
    [NonSerialized] public ChatBubbleHandler chatBubbleHandler;

    [SerializeField] private AudioClip musicLoop1;
    [SerializeField] private AudioClip musicLoop2;
    [SerializeField] private AudioClip musicLoop3;
    [SerializeField] private AudioClip musicLoop4;
    private AudioSource _audioSource;
    public Sprite winImage;
    [NonSerialized] public string gameState;
    private Timer _timer;
    private bool zombieIn;

    void Start()
    {
        zombieIn = false;
        _timer = GameObject.Find("timer_text").GetComponent<Timer>();
        gameState = "start_menu";
        rnd = new System.Random();
        humanSpawnRangeVertical = 8;
        humanSpawnRangeHorizontal = 15;
        zombieCount = 0;
        _audioSource = gameObject.GetComponent<AudioSource>();
        ScheduleMusic();
    }

    void StartGame()
    {
        _audioSource.clip = musicLoop1;
        _audioSource.Play();
        _timer.timerIsRunning = true;
        SpawnEntities();
        nuke.GetComponent<SpriteRenderer>().enabled = false;
        chatBubbleHandler = gameObject.GetComponent<ChatBubbleHandler>();
        GameObject startText = GameObject.Find("start_text");
        Destroy(startText);

    }

    IEnumerator DelayedChangeMusic(int seconds, AudioClip music)
    {
        yield return new WaitForSeconds(seconds);
        _audioSource.loop = false;
        yield return new WaitUntil(() => !_audioSource.isPlaying);
        _audioSource.clip = music;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void ScheduleMusic()
    {
        if (_timer.timeToNuke < timeToNuke / 4)
        {
            StartCoroutine(DelayedChangeMusic(0, musicLoop4));
        }
    }

    IEnumerator WaitForMobToLoad(GameObject mob)
    {
        yield return new WaitUntil(() => mob.GetComponent<HumanBase>().updating);
        if (mob.GetComponent<HumanBase>().spawnCollide)
        {
            Destroy(mob);
        }
    }

    IEnumerator ZombieLoad()
    {
        yield return new WaitForSeconds(0.1f);
        zombieIn = true;
    }

    void SpawnHumanBaseMobs(GameObject prefab, int numMobs)
    {
        // map is x ranges from -36 -> 46, y ranges from 26 -> -36
        for (int i = 1; i < numMobs; i++)
        {
            int rndX = rnd.Next(-36, 46);
            int rndY = rnd.Next(-36, 26);
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

    void SpawnPrez()
    {
        GameObject mob = Instantiate(prezPrefab, new Vector3(31, 17, -1), Quaternion.identity);
        StartCoroutine(WaitForMobToLoad(mob));
    }


    void SpawnEntities()
    {
        SpawnPrez();
        GameObject startZombie = Instantiate(zombiePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        StartCoroutine(ZombieLoad());
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

        if (gameState == "start_menu" && Input.GetMouseButtonDown(0))
        {
            gameState = "running";
            StartGame();
        }

        if (zombieIn  && zombieCount == 0)
        {
            QuitGame();
        }

        if (zombieCount > 4)
        {
            StartCoroutine(DelayedChangeMusic(0, musicLoop2));
        }

        if (zombieCount > 10)
        {
            StartCoroutine(DelayedChangeMusic(0, musicLoop3));
        }
    }

    public void Win()
    {
        nuke.GetComponent<SpriteRenderer>().sprite = winImage;
        nuke.GetComponent<SpriteRenderer>().enabled = true;
        GameObject camera = GameObject.Find("Main Camera");
        nuke.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -8);
        StartCoroutine(DelayQuit(20f));
    }

    IEnumerator DelayQuit(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        QuitGame();
    }

    IEnumerator DelayRestart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("SampleScene");
    }

    public void Lose()
    {
        nuke.GetComponent<SpriteRenderer>().enabled = true;
        GameObject camera = GameObject.Find("Main Camera");
        nuke.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -8);
        StartCoroutine(DelayRestart(5));
    }
}