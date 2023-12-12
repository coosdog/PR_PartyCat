using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    public CreateCar[] Maker = new CreateCar[2];
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            Debug.Log("µé¾î¿È");
            for(int i = 0; i < Maker.Length; i++) 
            {
                Maker[i].CancelInvoke();
                //Maker[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            for (int i = 0; i < Maker.Length; i++)
            {
                Maker[i].StartMake();
                //Maker[i].gameObject.SetActive(true);
            }
        }
    }
}
