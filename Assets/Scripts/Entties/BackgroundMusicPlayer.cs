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
                bgmSource.PlayOneShot(groundTheme);
                break;
            case GameState.Underground:
                bgmSource.Stop();
                bgmSource.loop = true;
                bgmSource.PlayOneShot(undergroundTheme);
                break;
            case GameState.MiddleTime:
                bgmSource.Stop();
                bgmSource.loop = false;
                bgmSource.PlayOneShot(levelCompleteTheme);
                break;
        }
    }
}
