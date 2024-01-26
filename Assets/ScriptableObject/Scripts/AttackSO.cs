using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData",menuName = "Ground_Mario/ Attacks/Default",order = 0)]//기본형 데이터 order는 순서
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")] 
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")] 
    public bool isInKnockBack;
    public float knockbackPower;
    public float knockbackTime;

}