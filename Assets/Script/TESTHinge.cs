using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTHinge : MonoBehaviour
{
    public HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        StartCoroutine(Cool());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Cool()
    {
        yield return new WaitForSeconds(3);
        joint.useMotor = true;
        yield return new WaitForSeconds(10);
        joint.useMotor = false;
    }

}
