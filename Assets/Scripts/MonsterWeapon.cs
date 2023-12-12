using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

public interface IHitable
{

    public void Hit(IActiveable activeable);
}

public interface IActiveable // 누군가를 때리는 상호작용
{
    public AttackStrategy AttackStrategy // 공격전략
    {
        get;
        set;
    }
    // public void Attack(IHitable hitable);
}

public class MonsterWeapon : MonoBehaviour, IActiveable
{
    public AttackStrategy AttackStrategy
    {
        get => attackStrategy;
        set
        {
            attackStrategy = value;
        }
    }
    private AttackStrategy attackStrategy;

    void Start()
    {
        // 나도 자동차 과자 주세오 - 괴도 미성이
        // 코드 우마이!
    }     
}


