using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip groundTheme;
    [SerializeField] private AudioClip undergroundTheme;
    [SerializeField] private AudioClip levelCompleteTheme;
    [SerializeField] private AudioClip deathSound;

    public void Start()
    {
        GameManager.Instance.OnStateChangeEvent += PlayMusic;
    }

    public void PlayMusic(GameState state)
    {
        switch (state)
        {
            case GameState.Ground:
                bgmSource.Stop();
                bgmSource.loop = true;
                bgmSource.clip = groundTheme;
                bgmSource.Play();
                break;
            case GameState.Underground:
                bgmSource.Stop();
                bgmSource.loop = true;
                bgmSource.clip = undergroundTheme;
                bgmSource.Play();
                break;
            case GameState.MiddleTime:
                bgmSource.Stop();
                bgmSource.loop = false;
                bgmSource.clip = levelCompleteTheme;
                bgmSource.Play();
                break;
            case GameState.Fail:
                bgmSource.Stop();
                bgmSource.loop = false;
                bgmSource.PlayOneShot(deathSound);
                break;
        }
    }
}
