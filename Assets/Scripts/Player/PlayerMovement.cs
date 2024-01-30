using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    LayerMask levelLayer;
    public bool _isFlying = false;
    public float maxFlyingDuration = 5f; // <- 이걸 어디에 배치하는게 제일 나으려나
    public float flyingDuration { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        levelLayer = LayerMask.NameToLayer("Level");
        _isFlying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFlying)
        {
            flyingDuration -= Time.deltaTime;

            if (flyingDuration < 0)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0);
                _isFlying = false;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelLayer.value == collision.gameObject.layer)
        {
            flyingDuration = maxFlyingDuration;
            Debug.Log("Player OnTrigger : Level");
        }
    }
}
