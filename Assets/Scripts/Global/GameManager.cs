using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerBase;

    private int _randomSeed;
    public int WaveCnt { get; private set; }
    public event Action OnStartGameEvent;
    public event Action OnWaveUpEvent; 
    public GameState gameState = GameState.Ground;
    public GameState playerState = GameState.Ground;
    public TilemapManager tilemapManager { get; private set; }
    public TimeScheduler timeScheduler { get; private set; }

   [SerializeField] public GameObject monsterSpawner;
    
    private void Awake()
    {
        Instance = this;
        tilemapManager = GetComponentInChildren<TilemapManager>();
        timeScheduler = new TimeScheduler();
        WaveCnt = 1;
        
        _randomSeed = (int)DateTime.Now.Ticks;
        Random.InitState(_randomSeed);
    }

    private void Start()
    {
        OnStartGameEvent += StartTimer;
        CallStartGameEvent();
    }

    public void CallStartGameEvent()
    {
        OnStartGameEvent?.Invoke();
    }

    private void StartTimer()
    {
        StartCoroutine(timeScheduler.Timer());
    }

    public void Waveup()
    {
        WaveCnt++;
        callWaveUpEvent();
    }

    public void callWaveUpEvent()
    {
        OnWaveUpEvent?.Invoke();
    }
}
