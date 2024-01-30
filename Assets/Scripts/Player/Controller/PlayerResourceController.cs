using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceController : MonoBehaviour
{
    [SerializeField] public Resource_Data _data;
    public void ChangeResource(Resource_Data data)
    {
        _data.AddResource(data);
    }
}
