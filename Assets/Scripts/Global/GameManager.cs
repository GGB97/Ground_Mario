using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerBase;
    PlayerInput playerInput;
    PlayerInput baseInput;

    private int _randomSeed;
    public int WaveCnt { get; private set; }
    public event Action OnStartGameEvent;
    public event Action OnWaveUpEvent;
    public event Action<GameState> OnStateChangeEvent;
    
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

        playerInput = player.GetComponent<PlayerInput>();
        baseInput = playerBase.GetComponent<PlayerInput>();

        playerInput.enabled = false;
        baseInput.enabled = true;

        Time.timeScale = 1;
    }

    private void Start()
    {
        OnStartGameEvent += StartTimer;
        OnStateChangeEvent += ChangeState;
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
        UIManager.instance.UpdateWaveUI();
        callWaveUpEvent();
    }

    public void callWaveUpEvent()
    {
        OnWaveUpEvent?.Invoke();
    }

    public void CallStateChangeEvent(GameState state)
    {
        OnStateChangeEvent?.Invoke(state);
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public void Switch()
    {
        if (playerInput.enabled)
            ClimbFromGround();
        else
            DigGround();
    }

    private void DigGround()
    {
        playerState = GameState.Underground;
        tilemapManager.StartDig();

        player.transform.position = new Vector3((int)(playerBase.transform.position.x - .5f), -8f, 0);
        baseInput.enabled = false;
        playerInput.enabled = true;
    }

    private void ClimbFromGround()
    {
        if (Mathf.Abs(baseInput.transform.position.x - player.transform.position.x) > 2 || player.transform.position.y < -9f)
            return;

        playerState = GameState.Ground;

        playerInput.enabled = false;
        baseInput.enabled = true;
    }
}
