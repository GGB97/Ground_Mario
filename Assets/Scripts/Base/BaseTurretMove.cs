using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretMove : MonoBehaviour
{
    public static BaseTurretMove instance;

    public event Action OnBaseMove;

    [SerializeField] private string baseTag = "Base";

    private GameObject _player;
    private GameObject _base;
    [SerializeField] public GameObject _turret;

    private void Awake()
    {
        instance = this;

        _turret = Instantiate(_turret);

        _base = GameObject.FindGameObjectWithTag(baseTag);

        OnBaseMove += MoveTurret;
    }

    private void Start()
    {
        OnBaseMove();
        _turret.SetActive(false);
    }

    public void CallBaseMoveEvent()
    {
        OnBaseMove?.Invoke();
    }



    private void MoveTurret()
    {
        var pos = _base.transform.position;
        pos.x += 2;
        pos.y += 1.4f;
        _turret.transform.position = pos;
    }

    


}
