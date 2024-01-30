using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretMove : MonoBehaviour
{
    public static BaseTurretMove instance;

    public event Action OnTurretMove;

    private GameObject _base;
    public GameObject _turret;

    private void Awake()
    {
        instance = this;

        _turret = Instantiate(_turret);
        _turret.SetActive(false);
        

        OnTurretMove += MoveTurret;
    }

    private void Start()
    {
        _base = GameManager.Instance.playerBase;
    }



    public void CallBaseMoveEvent()
    {
        OnTurretMove?.Invoke();
    }

 


    public void MoveTurret()
    {
        var pos = _base.transform.position;
        pos.y = -1.4f;
        _turret.transform.position = pos;
    }

    


}
