using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 _position;
    private Vector3 _targetPos;
    
    public float cameraZ = -10;
    public float speed = 8f;
    public int dayLimitY = 3;
    public int nightLimitY = -9;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        _position = GetPosition();
        
        var targetY = CalculateTargetY();
        _targetPos = new Vector3(_position.x, targetY, cameraZ);
        
        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.fixedDeltaTime * speed);
    }

    private Vector3 GetPosition()
    {
        switch (GameManager.Instance.playerState)
        {
            case GameState.Ground:
                _camera.orthographicSize = 8f;
                return GameManager.Instance.playerBase.transform.position;
            case GameState.Underground:
                _camera.orthographicSize = 5f;
                return GameManager.Instance.player.transform.position;
        }
        return GameManager.Instance.player.transform.position;
    }
    
    private float CalculateTargetY()
    {
        // 캐릭터 State에 따라 Clamp로 제한걸기.
        return GameManager.Instance.playerState switch
        {
            GameState.Ground => Mathf.Clamp(_position.y, dayLimitY, float.MaxValue),
            GameState.Underground => Mathf.Clamp(_position.y, float.MinValue, nightLimitY),
            _ => _position.y
        };
    }
}
