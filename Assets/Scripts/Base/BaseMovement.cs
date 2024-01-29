using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public static BaseMovement Instance;

    private BaseController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float dashDelay = 5f;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool IsDash = false;
    private bool IsDashable = true;

    private void Awake()
    {
        _controller = GetComponent<BaseController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsDashable)
        {
            Dash();
        }
        // �������� �ƴϾ �뽬�� �� -> ����� ��ư���� �ٲ㼭 �ذ�
        if (IsDash)
        {
            ApplyMovment(_movementDirection * 20);
            // ����� ��ư���� �مf���� �뽬�� �ȵ�
            //_rigidbody.AddForce(forcePower * _movementDirection.x, 0);
            IsDash = false;
            StartCoroutine(DashDelay());
        }
        else if (!IsDash)
        {
            ApplyMovment(_movementDirection);
        }

    }

    private void OnDirection(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x != 0)
            _spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
        OnDirection(_movementDirection);
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * moveSpeed;

        _rigidbody.velocity = direction;
    }
    private void Dash()
    {
        IsDash = true;
        IsDashable = false;
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDelay);
        IsDashable = true;
    }

}
