using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private GameObject _player; 
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _player = GameManager.Instance.player;
    }
    
    void Update()
    {
        float range = Vector3.Distance(_player.transform.position, transform.position);
        if (range < 3f)
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
