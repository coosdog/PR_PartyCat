using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    float tempZ;
    float originRotationZ;
    float targetZ;
    public bool isTest;
    public bool isPM;
    int moveSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        originRotationZ = transform.rotation.z;
        targetZ = -transform.rotation.z;
        if (targetZ < 0)
        {
            tempZ = -moveSpeed;
            isTest = true;
            isPM = true;
        }
        else
        {
            tempZ = moveSpeed;
            isTest = false;
            isPM = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
            transform.Rotate(new Vector3(0, 0, tempZ) * Time.deltaTime);
        if (isTest == true)
        {
            if (isPM == true)
            {

                if (transform.rotation.z < targetZ)
                {
                    tempZ *= -1f;
                    isTest = false;
                }
            }
            if(isPM == false)
            {
                if (transform.rotation.z < originRotationZ)
                {
                    tempZ *= -1f;
                    isTest = false;
                }
            }
        }
        if (isTest == false)
        {
            if (isPM == true)
            {
                if (transform.rotation.z > originRotationZ)
                {
                    tempZ *= -1f;
                    isTest = true;
                }
            }
            if (isPM == false)
            {
                if (transform.rotation.z > targetZ)
                {
                    tempZ *= -1f;
                    isTest = true;
                }
            }
        }




    }
}
