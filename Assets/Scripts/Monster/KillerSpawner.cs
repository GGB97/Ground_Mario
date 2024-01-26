using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    [SerializeField] private AttackSO KiierSO;

    private ProjectileManager _projectileManager;
    private float spawnposX;
    private float spawnposy;
    private float CurrentTime;
    
    void Start()
    {
        _projectileManager = ProjectileManager.instance;
    }
    
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > KiierSO.delay)
        {
            int randomspawh = Random.Range(0,5);
            for (int i = 0; i < randomspawh; i++)
            {
                spawnposX = Random.Range(-10, 10);
                spawnposy = 13f;
                RangedAttackData data = KiierSO as RangedAttackData;;
                Vector3 startpos = new Vector3(spawnposX, spawnposy, 0);
                _projectileManager.ShootBullet(startpos,new Vector2(transform.position.x,-1),data);                
            }
            CurrentTime = 0;
        }
    }
}
