using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDisapear : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private float fadeDuration = 2f;
    [SerializeField] private bool check;
    
    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += die;
    }

    void die()
    {
        StartCoroutine("disapear");
    }

    IEnumerator disapear()
    {
        _rigidbody2D.velocity = Vector3.zero; 
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            StartCoroutine("Fadeout", renderer); //TODO : 페이드아웃해줘서 초기화에서 알파값 돌려줘야됨.
        }
        
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }
        yield return new WaitForSeconds(2f);
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
}
