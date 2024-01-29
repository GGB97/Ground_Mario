using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groundSpawnPoint;
    [SerializeField] private GameObject[] skySpawnPoint;
    [SerializeField] private GameObject[] skyRallyPoint;
    [SerializeField] private GameObject MonsterPool;
    
    private ObjectPool _objectPool;
    private GameManager _gameManager;
    private Transform _playerPos;

    private float currentTime_Bomb;
    private float currentTime_Gumba;
    private float currentTime_HammerBros;
    private float currentTime_Kim;
    
    void Start()
    {
        _gameManager = GameManager.Instance; //낮밤 받아오기용 매니저.
        _objectPool = GetComponent<ObjectPool>();
    }
    
    //TODO : 1. 게임매니저에서 낮밤 구분 받아서 소환 할 지 말지 조건 걸어주고, 밤 시간동안 계속 스폰? 아니면 일정 마릿수만?
    void Update()
    {
        if (_gameManager.gameState == GameState.Ground) //TODO : 동작은 하지만, 시간이 끝나면 몹들이 사라지게 해야 될 것 같다.
        {
            currentTime_Bomb += Time.deltaTime;
            currentTime_Gumba += Time.deltaTime;
            currentTime_HammerBros += Time.deltaTime;
            currentTime_Kim += Time.deltaTime;
            SpawnMonster();
            MoveSpawnPosition();
        }
        else
        {
            DisableAllChildren();
            currentTime_Bomb = 0;
            currentTime_Gumba = 0;
            currentTime_HammerBros = 0;
            currentTime_Kim = 0;
        }
       
    }

    private void SpawnMonster()
    {
         if (currentTime_Bomb > 5)
        {
            //스폰은 이렇게 키면 될거같은데, 종류별 스폰을 어떻게 할까.
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("Bob_omb");
            GroundMonster_Bomb groundMonsterBomb = obj.GetComponent<GroundMonster_Bomb>();
            obj.SetActive(true);
            groundMonsterBomb.initiallize();
            obj.transform.position = pos.position;
            currentTime_Bomb = 0;
        }
        if (currentTime_Gumba > 3)
        {
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("Gumba");
            obj.transform.position = pos.position;
            GroundMonster_Contact groundMonsterContact = obj.GetComponent<GroundMonster_Contact>();
            obj.SetActive(true);
            groundMonsterContact.initiallize();
            currentTime_Gumba = 0;
        }
        if (currentTime_HammerBros > 10)
        {
            Transform pos = groundSpawnPoint[Random.Range(0, groundSpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("HammerBros");
            obj.transform.position = pos.position;
            GroundMonser_Range groundMonsterRange = obj.GetComponent<GroundMonser_Range>();
            obj.SetActive(true);
            groundMonsterRange.initiallize();
            currentTime_HammerBros = 0;
        }
        if (currentTime_Kim > 10)
        {
            Transform pos = skySpawnPoint[Random.Range(0, skySpawnPoint.Length)].transform;
            GameObject obj = _objectPool.SpawnFromPool("KimSuHanMu");
            FlyingMonster_Movement flyingMonsterMovement = obj.GetComponent<FlyingMonster_Movement>();
            FlyingMonster_Range flyingMonsterRange = obj.GetComponent<FlyingMonster_Range>();
            flyingMonsterMovement.SkyWay[0] = skyRallyPoint[0].transform;
            flyingMonsterMovement.SkyWay[1] = skyRallyPoint[1].transform;
            obj.transform.position = pos.position;
            obj.SetActive(true);
            flyingMonsterRange.initiallize();
            currentTime_Kim = 0;
        }
    }

    private void MoveSpawnPosition()
    {
        _playerPos = _gameManager.player.transform;
        groundSpawnPoint[0].transform.position = new Vector3(_playerPos.position.x-20,-3.4f,0f);
        groundSpawnPoint[1].transform.position = new Vector3(_playerPos.position.x+20,-3.4f,0f);
        skySpawnPoint[0].transform.position = new Vector3(_playerPos.position.x, 13f, 0);
        skyRallyPoint[0].transform.position = new Vector3(_playerPos.position.x-10,5f,0f);
        skyRallyPoint[1].transform.position = new Vector3(_playerPos.position.x+10,5f,0f);
    }
    
    private void DisableAllChildren()
    {
        // 모든 자식 오브젝트 끄기
        for (int i = 0; i < MonsterPool.transform.childCount; i++)
        {
            Transform childTransform = MonsterPool.transform.GetChild(i);
            childTransform.gameObject.SetActive(false);
        }
    }
}

