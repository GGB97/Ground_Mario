using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAniControl : MonoBehaviour
{
    public void DestroyProjectile()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
