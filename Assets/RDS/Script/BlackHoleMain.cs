using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleMain : MonoBehaviour
{
    public GameObject[] satellite = new GameObject[8];
    int count = 0;
    public bool isCheck = true;
    
    // Start is called before the first frame update
    void Start()
    {
        isCheck = true;
        for(int i = 0; i < transform.childCount; i++)
        {
        satellite[i] = transform.GetChild(i).gameObject;
        }
        StartCoroutine(BlackCool());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BlackCool()
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        GameObject temp = null;
        while (isCheck)
        {
            if (Input.GetKeyUp(KeyCode.L))
                isCheck = false;
            if (count == 8)
                count = 0;
            if(queue.Count == 3)
            {
                temp = queue.Dequeue();
                temp.gameObject.SetActive(false);
            }
            queue.Enqueue(satellite[count]);
            satellite[count].SetActive(true);
            count++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
