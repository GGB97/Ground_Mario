using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform player;
    
    public static GameManager Instance { get; private set; }
    public TilemapManager tilemapManager { get; private set; }
    
    public int seed;
    
    private void Awake()
    {
        Instance = this;
        tilemapManager = GetComponentInChildren<TilemapManager>();
        
        seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);
    }
}
