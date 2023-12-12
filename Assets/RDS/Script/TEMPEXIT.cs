using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPEXIT : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CarMove>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
