using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private GameObject _base; 
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _base = GameManager.Instance.playerBase;
    }
    
    void Update()
    {
        float range = Vector3.Distance(_base.transform.position, transform.position);
        if (range < 3f)
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
