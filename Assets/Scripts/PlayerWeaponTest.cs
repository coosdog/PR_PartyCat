using MonsterAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;

public abstract class PlayerAttackStrategy : MonsterAI.AttackStrategy
{
    protected Player player;
    public PlayerAttackStrategy(Player player)
    {
        this.player = player;
    }
}

public class GunAttackStrategy : PlayerAttackStrategy
{
    public GunAttackStrategy(Player player) : base(player)
    {
    }

    public override void Action()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(Player player)
    {
        // 날라가게 하는 전략이 있다.        
    }
}
public class SwordAttackStrategy : PlayerAttackStrategy
{
    public SwordAttackStrategy(Player player) : base(player)
    {
    }

    public override void Action()
    {
        
    }

    public override void Attack(Player player)
    {
        // 휘두르는 애니메이션을 실행하던가
        // 기절스택을 올림 and 날리거나
    }
}

public class PlayerWeaponTest : IActiveable
{
    public enum WEAPON_STATE
    {
        GUN,
        SWORD
    }

    public WEAPON_STATE WeaponState
    {
        get => weaponState;
        set
        {
            weaponState = value;
            switch (weaponState)
            {
                case WEAPON_STATE.GUN:                    
                    // AttackStrategy = new GunAttackStrategy();
                    break;
            }
        }
    }
    private WEAPON_STATE weaponState;    

    public MonsterAI.AttackStrategy AttackStrategy
    {
        get => attackStrategy;
        set
        {
            attackStrategy = value;
        }
    }
    private MonsterAI.AttackStrategy attackStrategy;
            
}
