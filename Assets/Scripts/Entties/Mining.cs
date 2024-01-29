using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private Tilemap tilemap;

    private LayerMask _levelLayerMask;
    private Vector3 _targetPos;
    private Vector3 _position;
    private Vector3 _direction;
    
    public float distance = 5f;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _levelLayerMask = LayerMask.GetMask("Level");
    }

    private void Start()
    {
        _controller.OnAttackEvent += TryMining;
    }

    private void TryMining(AttackSO attackSO)
    {
        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        _position = transform.position;
        _direction = (_targetPos - _position).normalized;

        var hit = Physics2D.Raycast(_position, _direction, distance, _levelLayerMask);
        if (hit.collider)
        {
            var target = (CustomRuleTile)tilemap.GetTile(tilemap.WorldToCell(
                new Vector2(hit.point.x - (hit.normal.x * 0.01f), hit.point.y - (hit.normal.y * 0.01f))));

            
        }
    }
}
