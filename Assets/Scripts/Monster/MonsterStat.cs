using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}
[Serializable]
public class MonsterStat
{
    public StatsChangeType StatsChangeType;
    [Range(0,100)]public int maxHealth; //인스펙터에서 조정할 수 있게
    [Range(0, 20f)] public float speed;
    
    //공격 데이터 aaa
    //그냥 해도 되는데 클래스 방식으로 구현하는 방식도 있겠지만 객체마다 자기 나름의 저장공간을 가지고있다
    //똑같은 몬스터가 100마리 1000마리가 됬을 때 공격 데이터도 다 할당을 해줘야되기때문에 복잡하고 무거움
    //그래서 우리는 스크립터블 오브젝트를 쓴다. 하나의 데이터 컨테이너를 만들어놓고 모두가 공유해 쓰는 것
    //훨씬 메모리적으로 간편하고 접근도 간편하며, 인스펙터 상에서 조절 가능하기때문에 훨씬 간편하다.
    public AttackSO attackSO;
    
}
