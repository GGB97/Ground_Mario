using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticleSystem;
    Transform particleTransform;
    Vector3 particlePos;

    private void Start()
    {
        particleTransform = dustParticleSystem.transform;
        particlePos = particleTransform.position;
    }

    public void CreateDustParticles()
    {
        particlePos.x = Mathf.Abs(particlePos.x);
        if(GetComponent<SpriteRenderer>().flipX == false)
            particlePos.x = -particlePos.x;
        particleTransform.localPosition = particlePos;

        if (createDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}