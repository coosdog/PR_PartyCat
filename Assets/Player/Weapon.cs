using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongWoo
{
    public interface IGribable
    {

    }

    public abstract class AttackStrategy
    {
        public Data data;

        public abstract void Attack();

        public abstract void SetStatus();
    }

    public struct Data
    {
        public float stunGage;
        public float atkSpeed;
    }


    public class Weapon : MonoBehaviour, IGribable
    {
        protected float stunGage;
        protected float atkSpeed;
        public AttackStrategy strategy;

        public virtual void SetStatus()
        {

        }

        public virtual void Use() { }
    }


    public class WeaponAttackStrategy : AttackStrategy
    {
        public override void Attack()
        {
            Debug.Log("ÈÖµÎ¸£´Â ¹«±â ½ÃÄö½º");
        }

        public override void SetStatus()
        {
            data.atkSpeed = 10;
            data.stunGage = 10;
        }
    }

    public class ShootingAttackStrategy : AttackStrategy
    {
        public override void Attack()
        {
            
        }

        public override void SetStatus()
        {
            data.atkSpeed = 10;
            data.stunGage = 10;
        }
    }

    



}




