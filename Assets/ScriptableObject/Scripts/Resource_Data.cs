using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource_", menuName = "Ground_Mario/Item/Default", order = 0)]//기본형 데이터 order는 순서
public class Resource_Data : ScriptableObject
{
    [Header("Item Info")]   // 나중에 추가될수도 있으니 일단 이걸로 만들어둠
    public int coin;



    public void AddResource(Resource_Data data)
    {
        this.coin += data.coin;
    }
}
