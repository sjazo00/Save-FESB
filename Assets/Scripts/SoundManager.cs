using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;
    static AudioSource audioSrc;
    private AudioClip[] coinSounds;
    private int randomCoinSound;
    public static AudioClip playerHitSound, enemyDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSounds = Resources.LoadAll<AudioClip>("CoinSounds");
        playerHitSound = Resources.Load<AudioClip>("player_hit");
        enemyDeathSound = Resources.Load<AudioClip>("enemydeath");
    }

    public void PlayCoinSound()
    {
        randomCoinSound = Random.Range(0, 5);
        audioSrc.PlayOneShot(coinSounds[randomCoinSound]);
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "player_hit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "enemydeath":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
        }

    }
}
