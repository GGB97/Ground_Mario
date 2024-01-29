using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAimController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform weaponPivot;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _spriteRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
