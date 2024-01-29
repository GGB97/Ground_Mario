using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float cameraZ = -10;
    public float speed = 5f;

    private void FixedUpdate()
    {
        var targetPos = new Vector3(player.transform.position.x, player.transform.position.y, cameraZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speed);
    }
}
