using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;

    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Animator animator;


    private void Start()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            this.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(this.variableJoystick.Horizontal, this.variableJoystick.Vertical) * Mathf.Rad2Deg, 0f); //ĳ���� ȸ�� 1206����
            if (Vector3.Magnitude(direction) <= 0.5)
            {
                animator.SetFloat("RelativeSpeed", 0.5f);   //���̽�ƽ ���� ũ�⿡ ���� �ִϸ��̼� ���� 1206����
            }
            else
            {
                animator.SetFloat("RelativeSpeed", 1f);
            }
        }
        else
        {
            animator.SetFloat("RelativeSpeed", 0f);
        }




    }
}
