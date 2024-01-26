using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterStatsHandler : MonoBehaviour
{
  [SerializeField] private MonsterStat _baseStats;
  public MonsterStat CurrentStates { get; private set; }
  public List<MonsterStat> statsModifiers = new List<MonsterStat>();//많이 사용되는 수정자 기억해두면 좋음
  
  private const float MinAttackDelay = 0.03f;
  private const float MinAttackPower = 0.5f;
  private const float MinAttackSize = 0.4f;
  private const float MinAttackSpeed = .1f;

  private const float MinSpeed = 0.8f;

  private const int MinMaxHealth = 5;

  private void Awake()
  {
    UpdateCharacterStats();
  }

  public void AddStatModifier(MonsterStat statModifier)
  {
      statsModifiers.Add(statModifier);
      UpdateCharacterStats();
  }
  
  public void RemoveStatModifier(MonsterStat statModifier) //뭔가 변화가 있을 때 스탯 업데이트를 함.
  {
      statsModifiers.Remove(statModifier);
      UpdateCharacterStats();
  }
  private void UpdateCharacterStats()
  {
    AttackSO attackSO = null;
    if (_baseStats.attackSO != null)
    {
      attackSO = Instantiate(_baseStats.attackSO); //지금 걸려있는 애를 가상으로 하나 더 복제함. 자유롭게 수정하기 위해 사용함. 거의 쓸 일 없긴하다고함.
    }

    CurrentStates = new MonsterStat{attackSO = attackSO};
    UpdateStats((a, b) => b, _baseStats); //오퍼레이션의 비밀, a,b를 받아서 b를 쓰겠다. 여기서 어떻게 처리하겠다는 정한거임.
    if (CurrentStates.attackSO != null)
    {
        CurrentStates.attackSO.target = _baseStats.attackSO.target;
    }
    
    foreach (MonsterStat modifier in statsModifiers.OrderBy(o => o.StatsChangeType))
    {
        if (modifier.StatsChangeType == StatsChangeType.Override)
        {
            UpdateStats((o, o1) => o1, modifier);
        }
        else if (modifier.StatsChangeType == StatsChangeType.Add)
        {
            UpdateStats((o, o1) => o + o1, modifier);
        }
        else if (modifier.StatsChangeType == StatsChangeType.Multiple)
        {
            UpdateStats((o, o1) => o * o1, modifier);
        }
    }

    LimitAllStats();

  }
  private void UpdateStats(Func<float, float, float> operation, MonsterStat newModifier) //펑션 : 매개 두개, 반환 하나
    {
        CurrentStates.maxHealth = (int)operation(CurrentStates.maxHealth, newModifier.maxHealth);
        CurrentStates.speed = operation(CurrentStates.speed, newModifier.speed);

        if (CurrentStates.attackSO== null || newModifier.attackSO == null)
           return;

        UpdateAttackStats(operation, CurrentStates.attackSO, newModifier.attackSO);

        if (CurrentStates.attackSO.GetType() != newModifier.attackSO.GetType())
        {
            return;
        }

        switch (CurrentStates.attackSO) //패턴 매칭, 패턴을 제공해주면 케이스에서 패턴을 쓸 수 있음
        {
            case RangedAttackData _:
                ApplyRangedStats(operation, newModifier); //여기서는 스탯변화가 플레이어만 일어나기때문에 원거리밖에 없음
                break;
        }
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }

    private void ApplyRangedStats(Func<float, float, float> operation, MonsterStat newModifier)
    {
        RangedAttackData currentRangedAttacks = (RangedAttackData)CurrentStates.attackSO;

        if (!(newModifier.attackSO is RangedAttackData))
        {
            return;
        }

        RangedAttackData rangedAttacksModifier = (RangedAttackData)newModifier.attackSO;
        currentRangedAttacks.multipleProjectilesAngel =
            operation(currentRangedAttacks.multipleProjectilesAngel, rangedAttacksModifier.multipleProjectilesAngel);
        currentRangedAttacks.spread = operation(currentRangedAttacks.spread, rangedAttacksModifier.spread);
        currentRangedAttacks.duration = operation(currentRangedAttacks.duration, rangedAttacksModifier.duration);
        currentRangedAttacks.numberofPorjectilesPerShot = Mathf.CeilToInt(operation(currentRangedAttacks.numberofPorjectilesPerShot,
            rangedAttacksModifier.numberofPorjectilesPerShot));
        currentRangedAttacks.projectileColor = UpdateColor(operation, currentRangedAttacks.projectileColor, rangedAttacksModifier.projectileColor);
    }

    private Color UpdateColor(Func<float, float, float> operation, Color currentColor, Color newColor)
    {
        return new Color(
            operation(currentColor.r, newColor.r),
            operation(currentColor.g, newColor.g),
            operation(currentColor.b, newColor.b),
            operation(currentColor.a, newColor.a));
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }

    private void LimitAllStats()
    {
        if (CurrentStates == null || CurrentStates.attackSO == null)
        {
            return;
        }

        LimitStats(ref CurrentStates.attackSO.delay, MinAttackDelay);
        LimitStats(ref CurrentStates.attackSO.power, MinAttackPower);
        LimitStats(ref CurrentStates.attackSO.size, MinAttackSize);
        LimitStats(ref CurrentStates.attackSO.speed, MinAttackSpeed);
        LimitStats(ref CurrentStates.speed, MinSpeed);
        CurrentStates.maxHealth = Mathf.Max(CurrentStates.maxHealth, MinMaxHealth);
    }
}



  



