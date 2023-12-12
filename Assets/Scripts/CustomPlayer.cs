using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayer : MonoBehaviour,ICustomHitable
{
    public Rigidbody rb;
    public Rigidbody Rb
    {
        get
        {
            return rb;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


}
