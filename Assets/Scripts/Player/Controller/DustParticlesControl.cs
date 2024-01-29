using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticleSystem;

    SpriteRenderer spriteRenderer;
    Transform particleTransform;
    Vector3 particlePos;

    private void Start()
    {
        particleTransform = dustParticleSystem.transform;
        particlePos = particleTransform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CreateDustParticles()
    {
        if (createDustOnWalk)
        {
            particlePos.x = Mathf.Abs(particlePos.x);
            if (spriteRenderer.flipX == false)
                particlePos.x = -particlePos.x;

            particleTransform.localPosition = particlePos;

            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}