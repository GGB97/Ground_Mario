using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groundSpawnPoint;
    [SerializeField] private GameObject[] skySpawnPoint;
    [SerializeField] private GameObject[] skyRallyPoint;
    
    private ObjectPool _objectPool;
    private GameManager _gameManager;

    private float currentTime_Bomb;
    private float currentTime_Gumba;
    private float currentTime_HammerBros;
    private float currentTime_Kim;
    
    void Start()
    {
        _gameManager = GameManager.Instance; //낮밤 받아오기용 매니저.
        _objectPool = GetComponent<ObjectPool>();
    }
    
    //TODO : 1. 게임매니저에서 낮밤 구분 받아서 소환 할 지 말지 조건 걸어주고 2. 몬스터 종류에 따라 위치를 다르게 설정해야됨.
    void Update()
    {
        currentTime_Bomb += Time.deltaTime;
        currentTime_Gumba += Time.deltaTime;
        currentTime_HammerBros += Time.deltaTime;
        currentTime_Kim += Time.deltaTime;
        if (currentTime_Bomb > 5)
        {
            //스폰은 이렇게 키면 될거같은데, 종류별 스폰을 어떻게 할까.
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("Bob_omb");
            GroundMonster_Bomb groundMonsterBomb = obj.GetComponent<GroundMonster_Bomb>();
            groundMonsterBomb.initiallize();
            obj.transform.position = pos.position;
            obj.SetActive(true);
            currentTime_Bomb = 0;
        }
        if (currentTime_Gumba > 3)
        {
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("Gumba");
            obj.transform.position = pos.position;
            obj.SetActive(true);
            currentTime_Gumba = 0;
        }
        if (currentTime_HammerBros > 10)
        {
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("HammerBros");
            obj.transform.position = pos.position;
            obj.SetActive(true);
            currentTime_HammerBros = 0;
        }
        if (currentTime_Kim > 10)
        {
            Transform pos = skySpawnPoint[Random.Range(0, skySpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("KimSuHanMu");
            FlyingMonster_Movement flyingMonsterMovement = obj.GetComponent<FlyingMonster_Movement>();
            flyingMonsterMovement.SkyWay[0] = skyRallyPoint[0].transform;
            flyingMonsterMovement.SkyWay[1] = skyRallyPoint[1].transform;
            obj.transform.position = pos.position;
            obj.SetActive(true);
            currentTime_Kim = 0;
        }
    }
}

