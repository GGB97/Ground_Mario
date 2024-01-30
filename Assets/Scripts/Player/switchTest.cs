using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class switchTest : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerInput baseInput;
    private GameManager _gameManager;
    private GameObject _player;
    private GameObject _playerBase;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(Click);
        playerInput.enabled = false;
        baseInput.enabled = true;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;;
        _player = _gameManager.player;
        _playerBase = _gameManager.playerBase;
    }

    public void Click()
    {
        if(playerInput.enabled)
            ClimbFromGround();
        else
            DigGround();
    }

    private void DigGround()
    {
        _gameManager.playerState = GameState.Underground;
        _gameManager.tilemapManager.StartDig();
        
        _player.transform.position = new Vector3((int)(_playerBase.transform.position.x - .5f), -8f, 0);
        
        baseInput.enabled = false;
        playerInput.enabled = true;
    }

    private void ClimbFromGround()
    {
        if (Mathf.Abs(_playerBase.transform.position.x - _player.transform.position.x) > 2 || _player.transform.position.y < -9f)
            return;
        
        _gameManager.playerState = GameState.Ground;
        
        playerInput.enabled = false;
        baseInput.enabled = true;
    }
}
