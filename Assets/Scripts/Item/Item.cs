using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer; // 이것도 so 안에 넣어버리면 좋으려나?
    [SerializeField] Resource_Data _data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            collision.GetComponent<PlayerResourceController>().ChangeResource(_data);
            DestroyItem();
            UIManager.instance.UpdateCoinUI();
        }
    }


    bool IsPlayer(Collider2D collision)
    {
        return playerLayer == (playerLayer | (1 << collision.gameObject.layer));
    }

    void DestroyItem()
    {
        this.gameObject.SetActive(false);
    }
}
