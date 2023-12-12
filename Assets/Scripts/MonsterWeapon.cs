using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

public interface IHitable
{

    public void Hit(IActiveable activeable);
}

public interface IActiveable // �������� ������ ��ȣ�ۿ�
{
    public AttackStrategy AttackStrategy // ��������
    {
        get;
        set;
    }
    // public void Attack(IHitable hitable);
}

public class MonsterWeapon : MonoBehaviour, IActiveable
{
    public AttackStrategy AttackStrategy
    {
        get => attackStrategy;
        set
        {
            attackStrategy = value;
        }
    }
    private AttackStrategy attackStrategy;

    void Start()
    {
        // ���� �ڵ��� ���� �ּ��� - ���� �̼���
        // �ڵ� �츶��!
    }     
}


