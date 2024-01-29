using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    [SerializeField] private AttackSO KiierSO;

    private ProjectileManager _projectileManager;
    private GameManager _gameManager;
    private Transform _pivot;
    
    
    private float spawnposX;
    private float spawnposy;
    private float CurrentTime;

    void Start()
    {
        _projectileManager = ProjectileManager.instance;
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (_gameManager.gameState == GameState.Ground)
        {
            CurrentTime += Time.deltaTime;
            _pivot = _gameManager.player.transform;
            if (CurrentTime > KiierSO.delay) 
            {
                spawnposX = Random.Range(_pivot.position.x - 10, _pivot.position.x + 10);
                spawnposy = _gameManager.player.transform.position.y + 13f;
                RangedAttackData data = KiierSO as RangedAttackData;
                Vector3 startpos = new Vector3(spawnposX, spawnposy, 0);
                _projectileManager.ShootBullet(startpos, new Vector2(transform.position.x, -1), data);
                CurrentTime = 0;
            }
        }
        else
        {
            CurrentTime = 0;
        }
    }
}