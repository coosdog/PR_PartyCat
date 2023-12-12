using JongWoo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player : MonoBehaviour
{
    public Transform weaponSpot;
    bool isGrab;
    public bool isStun;
    public Rigidbody rb;
    //WeaponController weapon;
    AttackStrategy curStrategy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IGribable weapon))
        {
            if (weapon != null)
            {
                curStrategy = other.transform.GetComponent<Weapon>().strategy;
                other.transform.SetParent(weaponSpot);
                other.transform.position = weaponSpot.position;
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (curStrategy != null)
            curStrategy.Attack();
        else
            Debug.Log("аж╦т");
    }
}
