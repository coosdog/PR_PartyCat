using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;

public class Weapon_Hit : Weapon
{
    public override void SetStatus()
    {
        //���ϰ����� ������ �ʱ�ȭ �� ����
    }

    private void Start()
    {
        strategy = new WeaponAttackStrategy();
    }
}