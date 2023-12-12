using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlackHole : MonoBehaviour
{
    Collider[] cols;
    float rid = 10f;
    Vector3 dir;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        cols = Physics.OverlapSphere(transform.position, rid, 1 << 6 | 1 << 7);

    }

    // Update is called once per frame
    void Update()
    {
        Pull();
    }

    void Pull()
    {
        foreach (Collider col in cols) 
        {
            col.GetComponent<Player>().rb.useGravity = false;
            dir = transform.position - col.transform.position;
            col.transform.position +=  new Vector3(dir.x,dir.y-1,dir.z) * Time.deltaTime;
            //col.transform.rotation = Quaternion.Euler(90,transform.rotation.y,transform.rotation.z);
            col.transform.Rotate(new Vector3(0, 1, 0));
        }
    }
    IEnumerator BH()
    {
        yield return 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rid);
    }
}
