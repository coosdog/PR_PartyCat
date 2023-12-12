using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomAttackable
{
    GameObject Obj { get; }
    public void Attack(ICustomHitable hitable);
}

public interface IPassiveAttackable : ICustomAttackable
{

}

public interface IActiveAttackable : ICustomAttackable
{

}

public abstract class AttackStrategy : ICustomAttackable
{
    public GameObject obj;
    public AttackStrategy(GameObject obj)
    {
        this.obj = obj;
    }
    public GameObject Obj => obj;
    public abstract void Attack(ICustomHitable hitable);
}

public class AddForceAttackStrategy : AttackStrategy, IPassiveAttackable
{
    public AddForceAttackStrategy(GameObject obj) : base(obj)
    {
    }
    public override void Attack(ICustomHitable hitable)
    {
        Vector3 dir = obj.transform.position - hitable.Rb.transform.position;
        hitable.Rb.AddForce(dir);
    }
}
public class CustomGunAttackStrategy : AttackStrategy , IActiveAttackable
{
    public CustomGunAttackStrategy(GameObject obj) : base(obj)
    {

    }
    public override void Attack(ICustomHitable hitable)
    {
        CustomPlayer customPlayer = obj.GetComponent<CustomPlayer>();
        Bullet bullet = GameObject.Instantiate(bulletPrafab);
        
    }
}
public interface ICustomHitable
{
    public Rigidbody Rb { get; }
}

public class CustomMonWeapon 
{
    public ICustomAttackable attackableStrategy;

    public void Attack(ICustomHitable hitable)
    {
        attackableStrategy.Attack(hitable);
    }
}

public class CustomMon : MonoBehaviour, ICustomHitable
{
    CustomMonWeapon weapon;
    Rigidbody rb; 
    public Rigidbody Rb => rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon = new CustomMonWeapon();
        weapon.attackableStrategy = new AddForceAttackStrategy(gameObject);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHitable hitable))
        {
            if(weapon.attackableStrategy is IPassiveAttackable)
                weapon.Attack(this);
        }
    }
}
