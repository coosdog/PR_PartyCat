using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCar : MonoBehaviour
{
    public GameObject Prefab;
    public float CreatStartTime;
    public float CreatCoolTime;
    void Start()
    {
        StartMake();
    }

    public void CarMake()
    {
        Instantiate(Prefab,transform.position,transform.rotation);
    }
    public void StartMake()
    {
        InvokeRepeating("CarMake", CreatStartTime, CreatCoolTime);
    }
}
