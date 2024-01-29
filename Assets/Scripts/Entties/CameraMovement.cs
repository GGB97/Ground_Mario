using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float cameraZ = -10;
    public float speed = 5f;

    //TODO 기지때의 시점과 플레이어 때의 시점, 카메라 위치 제한두기.
    private void FixedUpdate()
    {
        var position = player.transform.position;
        var targetPos = new Vector3(position.x, position.y, cameraZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speed);
    }
}
