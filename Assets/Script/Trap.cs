using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapMode
{
    protected Trap owner;
    public TrapMode(Trap trap)
    {
        owner = trap;
    }
    public abstract void Function();
}
public class ST_CrossWalk : TrapMode
{
    public ST_CrossWalk(Trap trap) : base(trap) { }
    public override void Function()
    {
        throw new System.NotImplementedException();
    }
}
public enum TrapType
{
    ST_CrossWalk = 1<<0
}

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
