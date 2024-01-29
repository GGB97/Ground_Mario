using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RangedAttackData",menuName = "Ground_Mario/ Attacks/Ranged",order = 1)]//기본형 데이터 order는 순서
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")] 
    public string bulletNameTag;

    public float duration;
    public float spread;
    public int numberofPorjectilesPerShot;
    public float multipleProjectilesAngel;
    public Color projectileColor;
}