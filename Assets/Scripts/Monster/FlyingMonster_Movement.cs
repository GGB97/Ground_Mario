using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingMonster_Movement : MonoBehaviour
{
    CharacterController _controller;

    Vector2 _moventDirection = Vector2.zero;
    Rigidbody2D _rigidbody;

    Vector2 _knockback = Vector2.zero;
    float knockbackDuration = 0;
    private CharStatsHandler _monsterStatsHandler;
    [SerializeField] public Transform[] SkyWay;
    Transform newdes;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _monsterStatsHandler = GetComponent<CharStatsHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
        newdes = SkyWay[Random.Range(0, SkyWay.Length)];
    }

    private void FixedUpdate()
    {
        //ApplyMovent(_moventDirection);
    }

    void Move(Vector2 direction)
    {
        newdes = GetDestination();
        Vector3 position = newdes.position;
        StartCoroutine("GetMoving", position);
    }

    IEnumerator GetMoving(Vector3 pos)
    {
        while (true)
        {
            if (Mathf.Abs(pos.x - this.transform.position.x) < 0.1f) //근처에 가면 정지 - 좌표값을 빼는 개념이라 애매함.
            {
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, _monsterStatsHandler.CurrentStates.speed); //이동할 때는 이렇게
            // Vector3 speed = Vector3.zero;
            // transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed,3f);
            
            yield return new WaitForFixedUpdate();
        }
        //this.transform.Translate(transform.position.x, transform.position.y, 0);
        //transform.position += pos * Time.fixedDeltaTime;
        //yield return null;
        //yield return new WaitUntil(()=> Mathf.Abs(transform.position.x - this.transform.position.x) < 1f);
        // if (Mathf.Abs(transform.position.x - this.transform.position.x) < 1f)
        //     yield break;
    }

    // void ApplyMovent(Vector2 direction)
    // {
    //     direction = direction * _monsterStatsHandler.CurrentStates.speed;
    //   
    //     _rigidbody.velocity = direction;
    // }

    protected Transform GetDestination()
    {
        if (newdes == SkyWay[0])
            newdes = SkyWay[1];
        else newdes = SkyWay[0];
        return newdes;
    }
}