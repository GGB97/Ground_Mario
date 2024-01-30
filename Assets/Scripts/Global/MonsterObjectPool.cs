using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterObjectPool : ObjectPool
{
    protected override void Awake()
    {
        poolDictionary = new();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                if (pool.prefab.CompareTag("Enemy"))
                {
                    int index = 0;
                    switch (pool.prefab.name)
                    {
                        case "Monster_Gumba":
                            index = 0;
                            break;
                        case "Bob_omb":
                            index = 1;
                            break;
                        case "Monster_HammerBros":
                            index = 2;
                            break;
                        case "KimSuHanMu":
                            index = 3;
                            break;
                    }
                    GameObject obj = Instantiate(pool.prefab, parents[index]);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnWaveUpEvent += MonsterUpgrade;
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);
        

        return obj;
    }

    private void MonsterUpgrade()
    {
        var Stats = GameManager.Instance.monsterSpawner.GetComponent<MonsterSpawner>();
        Stats.HealthUpgrade();
        if (GameManager.Instance.WaveCnt != 1)
        {
            foreach (var bomb in poolDictionary["Bob_omb"])
             {
                 bomb.GetComponent<CharStatsHandler>().AddStatModifier(Stats.BombStat);
             }
            
             foreach (var kim in poolDictionary["KimSuHanMu"])
             {
                 kim.GetComponent<CharStatsHandler>().AddStatModifier(Stats.KimStat);
             }
            
            foreach (var gumba in poolDictionary["Gumba"])
            {
                gumba.GetComponent<CharStatsHandler>().AddStatModifier(Stats.DefaultStat);
            }
            
            foreach (var hammer in poolDictionary["HammerBros"])
            {
                hammer.GetComponent<CharStatsHandler>().AddStatModifier(Stats.HammerbrosStat);
            }
          
        }
    }
}