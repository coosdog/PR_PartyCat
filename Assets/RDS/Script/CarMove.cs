using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    int moveSpeed = 30;
    int attackPower = 1000;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null) 
        {
            Debug.Log("�浹!");
            //collision.rigidbody.AddForce(new Vector3(0,0,-1000)*Time.deltaTime,ForceMode.Impulse);
            //�����ʿ� �����̻���. ���� ������ġ�� ����
        }
    }
}
