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
        Debug.Log("»Ð»Ð ½î´Â ¹«±â ½ÃÄö½º");
    }

    public void Start()
    {
        strategy = new ShootingAttackStrategy();
    }

}
