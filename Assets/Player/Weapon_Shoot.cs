using JongWoo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon_Shoot : Weapon
{
    public GameObject bullet;

    public override void SetStatus() { }

    public override void Use()
    {
        Debug.Log("�л� ��� ���� ������");
    }

    public void Start()
    {
        strategy = new ShootingAttackStrategy();
    }

}
