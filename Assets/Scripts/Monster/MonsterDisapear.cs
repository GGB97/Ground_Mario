using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDisapear : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Monster_Movement _monsterMovement;
    private FlyingMonster_Movement _flyingMonsterMovement;
    private CharacterController _characterController;
    public bool isDie { get; private set; }

    private float fadeDuration = 2f;
    
    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _monsterMovement = GetComponent<Monster_Movement>();
        _flyingMonsterMovement = GetComponent<FlyingMonster_Movement>();
        _characterController = GetComponent<CharacterController>();
        _healthSystem.OnDeath += die;
    }

    public void die()
    {
        if (!isDie)
        {
            isDie = true;
            if (_monsterMovement != null)
                _monsterMovement.enabled = false;
            if (_flyingMonsterMovement != null)
                _flyingMonsterMovement.enabled = false;
            _characterController.enabled = false;
            StartCoroutine("disapear");    
        }
        
    }

    IEnumerator disapear()
    {
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            StartCoroutine("Fadeout", renderer); //TODO : 페이드아웃해줘서 초기화에서 알파값 돌려줘야됨.
        }
        yield return new WaitForSeconds(2f);
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }
        gameObject.SetActive(false);
    }

    IEnumerator Fadeout(SpriteRenderer renderer)
    {
        float timer = 0f;
        Color startColor = renderer.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, progress));
            renderer.color = newColor;
            yield return null;
        }
    }

    public void reset()
    {
        isDie = false;
    }
}
