using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioClip zombieGroan1;
    public AudioClip zombieGroan2;
    public AudioClip zombieGroan3;
    public AudioClip zombieGroan4;
    public AudioClip zombieBite1;
    public AudioClip zombieBite2;
    private AudioSource _audioSource;
    private System.Random rnd;
    private AudioClip[] groans;
    private AudioClip[] bites;
    public int sampleSize;
    private GameController gameController;


    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        groans = new[]
            {
                zombieGroan1, zombieGroan2, zombieGroan3, zombieGroan4
            };
        bites = new[]
        {
            zombieBite1, zombieBite2
        };
        rnd = new System.Random();
    }

    private AudioClip RandomZombieSound(string type)
    {
        AudioClip sound;
        if (type == "bite")
        {
            int choice = rnd.Next(bites.Length);
            sound = bites[choice];
        }
        else
        {
            int choice = rnd.Next(groans.Length);
            sound = groans[choice];
        }

        return sound;
    }

    public void BiteNoise()
    {
        StartCoroutine(DelayPlay(0f, RandomZombieSound("bite"), false));
    }

    public void GroanNoise()
    {
        StartCoroutine(DelayPlay(0f, RandomZombieSound("groan"), true));

    }


    IEnumerator DelayPlay(float delay, AudioClip sound, bool okayToSkip)
    {
        if (okayToSkip && _audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(delay);
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    private void Update()
    {
        if (gameController.gameState == "running" && rnd.Next(sampleSize) == 0)
        {
            GroanNoise();
        }
    }
}