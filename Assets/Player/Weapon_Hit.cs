using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;

public class Weapon_Hit : Weapon
{
    public override void SetStatus()
    {
        //스턴게이지 같은거 초기화 후 세팅
    }

    private void Start()
    {
        strategy = new WeaponAttackStrategy();
    }
}