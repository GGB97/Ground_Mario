using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimationEvent : MonoBehaviour
{
    public void DestroyBase()
    {
        Time.timeScale = 0;
    }
}
