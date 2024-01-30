using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    protected Animator[] animator;
    protected CharacterController controller;
    protected HealthSystem healthSystem;

    protected virtual void Awake()
    {
        animator = GetComponentsInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>();
    }
}
