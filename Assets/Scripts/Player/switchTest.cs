using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class switchTest : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerInput baseInput;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(Click);
        playerInput.enabled = true;
        baseInput.enabled = false;
    }

    public void Click()
    {
        if(playerInput.enabled == true)
        {
            GameManager.Instance.playerState = GameState.Ground;
            playerInput.enabled = false;
            baseInput.enabled = true;
        }
        else
        {
            GameManager.Instance.playerState = GameState.Underground;
            DigGround();
            baseInput.enabled = false;
            playerInput.enabled = true;
        }
    }

    private void DigGround()
    {
        var player = GameManager.Instance.player;
        var playerBase = GameManager.Instance.playerBase;
        
        GameManager.Instance.tilemapManager.StartDig();
        
        player.transform.position = new Vector3((int)playerBase.transform.position.x, -8f, 0);
    }
}
