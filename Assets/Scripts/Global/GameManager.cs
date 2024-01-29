using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public GameObject player;

    private int _randomSeed;
    public event Action OnStartGameEvent;
    public GameState gameState = GameState.Ground;
    public TilemapManager tilemapManager { get; private set; }
    public TimeScheduler timeScheduler { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        tilemapManager = GetComponentInChildren<TilemapManager>();
        timeScheduler = new TimeScheduler();
        
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
}
