using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 _position;
    private Vector3 _targetPos;
    
    public float cameraZ = -10;
    public float speed = 5f;
    public int dayLimitY = 0;
    public int nightLimitY = -9;

    private void FixedUpdate()
    {
        _position = player.transform.position;
        
        var targetY = CalculateTargetY();
        _targetPos = new Vector3(_position.x, targetY, cameraZ);
        
        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.fixedDeltaTime * speed);
    }
    
    private float CalculateTargetY()
    {
        // 낮 밤에 따라 Clamp로 제한걸기.
        return GameManager.Instance.gameState switch
        {
            GameState.Ground => Mathf.Clamp(_position.y, dayLimitY, float.MaxValue),
            GameState.Underground => Mathf.Clamp(_position.y, float.MinValue, nightLimitY),
            _ => _position.y
        };
    }
}
