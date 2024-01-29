using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseAnimations : MonoBehaviour
{
    private Animator animator;
    private BaseController controller;

    private static readonly int IsDash = Animator.StringToHash("IsDash");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<BaseController>();
    }

    private void Start()
    {
        //controller.OnDashEvent += Dash;
    }

    private void Dash(Vector2 obj)
    {
        animator.SetTrigger(IsDash);
    }
}
