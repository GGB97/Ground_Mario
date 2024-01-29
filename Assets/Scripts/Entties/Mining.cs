using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject destroyStage;

    private LayerMask _levelLayerMask;
    private Vector3 _mousePos;
    private Vector3 _position;
    private Vector3 _direction;
    private Vector3Int _targetPos;
    private Vector3Int? _recentHit;
    private CustomRuleTile _target;
    
    public float distance = 5f;
    
    private void Awake()
    {
        _levelLayerMask = LayerMask.GetMask("Level");
    }

    private void Update()
    {
        TryMining();
    }

    private void TryMining()
    {
        if (!Input.GetMouseButton(0))
        {
            DeactivateDestroyStage();
            _recentHit = null;
            return;
        }

        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _position = transform.position;
        _direction = (_mousePos - _position).normalized;

        var hit = Physics2D.Raycast(_position, _direction, distance, _levelLayerMask);
        if (!CheckCorrectTile(hit))
            return;
        
        ProcessTileDestruction(_target, _targetPos);
    }

    private bool CheckCorrectTile(RaycastHit2D hit)
    {
        if (!hit.collider)
        {
            DeactivateDestroyStage();
            _recentHit = null;
            return false;
        }
        
        _targetPos = tilemap.WorldToCell(new Vector2(hit.point.x - (hit.normal.x * 0.01f), hit.point.y - (hit.normal.y * 0.01f)));
        
        if (_recentHit == _targetPos)
        {
            return false;
        }

        DeactivateDestroyStage();
        _recentHit = _targetPos;
        
        _target = tilemap.GetTile<CustomRuleTile>(_targetPos); //_target = (CustomRuleTile)tilemap.GetTile(_targetPos);
        return true;
    }
    
    private void DeactivateDestroyStage()
    {
        if (destroyStage.activeSelf)
            destroyStage.SetActive(false);
    }
    
    private void ProcessTileDestruction(CustomRuleTile target, Vector3Int targetPos)
    {
        if (target.isBreakable && !destroyStage.activeSelf)
        {
            destroyStage.GetComponent<Animator>().speed = 1f / target.hardness;
            destroyStage.GetComponent<DestroyTile>().targetPos = targetPos;
            destroyStage.transform.position = targetPos + new Vector3(0.5f, 0.5f);
            destroyStage.SetActive(true);
        }
    }
}
