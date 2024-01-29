using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    [SerializeField] public GameObject player;

    public TilemapManager tilemapManager { get; private set; }

    public int seed;

    private void Awake()
    {
        Instance = this;
        tilemapManager = GetComponentInChildren<TilemapManager>();
        
        seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
