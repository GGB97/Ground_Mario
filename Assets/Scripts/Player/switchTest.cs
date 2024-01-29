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
            playerInput.enabled = false;
            baseInput.enabled = true;
        }
        else
        {
            baseInput.enabled = false;
            playerInput.enabled = true;
        }
    }
}
