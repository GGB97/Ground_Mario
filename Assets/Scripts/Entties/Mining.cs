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
        // 마우스를 누르는 중일 때만 코드 처리
        if (!Input.GetMouseButton(0) || GameManager.Instance.playerState != GameState.Underground)
        {
            // 마우스를 때면 일시정지 처리
            DeactivateDestroyStage();
            _recentHit = null;
            return;
        }

        // 마우스 위치
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _position = transform.position;
        _direction = (_mousePos - _position).normalized;

        // 타일의 정보 확인하기
        var hit = Physics2D.Raycast(_position, _direction, distance, _levelLayerMask);
        if (!CheckCorrectTile(hit))
            return;
        
        ProcessTileDestruction(_target, _targetPos);
    }

    private bool CheckCorrectTile(RaycastHit2D hit)
    {
        // null 값일 때
        if (!hit.collider)
        {
            DeactivateDestroyStage();
            _recentHit = null;
            return false;
        }
        
        // tile의 position
        _targetPos = tilemap.WorldToCell(new Vector2(hit.point.x - (hit.normal.x * 0.01f), hit.point.y - (hit.normal.y * 0.01f)));
        
        // 최근에 검출된 hit과 target이 같을 때(계속 같은 타일을 누르는 중일 때)
        if (_recentHit == _targetPos)
        {
            return false;
        }

        // 새로운 타일 발견
        DeactivateDestroyStage();
        _recentHit = _targetPos;
        
        _target = tilemap.GetTile<CustomRuleTile>(_targetPos); //_target = (CustomRuleTile)tilemap.GetTile(_targetPos);
        return true;
    }
    
    private void DeactivateDestroyStage()
    {
        // 애니메이션 처리된 destroyStage 비활성화
        if (destroyStage.activeSelf)
            destroyStage.SetActive(false);
    }
    
    // Tile 파괴 처리
    private void ProcessTileDestruction(CustomRuleTile target, Vector3Int targetPos)
    {
        if (target.isBreakable && !destroyStage.activeSelf)
        {
            destroyStage.GetComponent<Animator>().speed = 2f / target.hardness;
            destroyStage.GetComponent<DestroyTile>().targetPos = targetPos;
            destroyStage.transform.position = targetPos + new Vector3(0.5f, 0.5f);
            destroyStage.SetActive(true);
        }
    }
}
