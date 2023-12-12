using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.AI;

namespace MonsterAI
{
        
    public interface IStateMachine
    {
        void SetState(string name); // 상태바꾸는 함수
        object GetOwner(); // 제너릭이기 때문에 
    }

    public class State
    {
        public IStateMachine sm;

        public virtual void Init(IStateMachine sm)
        {
            this.sm = sm;
        }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
    }
    public class MonsterState : State
    {
        protected Monster monster;
        protected Vector3 targetPos;
        public override void Init(IStateMachine sm)
        {
            base.Init(sm);
            monster = (Monster)sm.GetOwner();
        }                
    }

    public class MonsterMoveState : MonsterState
    {
        const float RANGE_DIS = 5f;
        public override void OnEnter()
        {
            monster.targetCol = null;
            targetPos = monster.transform.position;
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {            
            Collider[] cols = Physics.OverlapSphere(monster.transform.position, 10f, 1 << 6);            
            if (NearEnemySearch(cols))
            {
                sm.SetState("Chase");
                return;
            }            
            Debug.DrawLine(new Vector3(targetPos.x, targetPos.y + 10, targetPos.z), targetPos);
            monster.agent.SetDestination(targetPos);
            //Debug.Log("거리" + (monster.transform.position - targetPos).magnitude);


            //nowTime += Time.deltaTime;
            //if (nowTime >= maxTime)
            //{
            //    // targetPos = SetRandPos();
            //    idx++;
            //    targetPos = monster.posArr[idx];
            //    nowTime = 0;
            //}
            if ((monster.transform.position - targetPos).magnitude <= RANGE_DIS)
            {
                // targetPos = SetRandPos();
                Vector3 tempPos;
                while (true)
                {
                    tempPos = Random.insideUnitSphere * 49f; // 반지름이 50인 원범위안의 랜덤한 위치 
                    tempPos.y = 10f;
                    if (!Physics.Raycast(tempPos, -Vector3.up, 11f, 1 << 7))
                    {
                        tempPos.y = 0;
                        targetPos = tempPos;
                        break;
                    }
                }
            }

        }

        public Vector3 SetRandPos()
        {
            Vector3 tempPos;
            while (true)
            {
                tempPos = Random.insideUnitSphere * 49f; // 반지름이 50인 원범위안의 랜덤한 위치 
                tempPos.y = 10f;
                if (!Physics.Raycast(tempPos, -Vector3.up, 11f, 1 << 7))
                {
                    tempPos.y = 0;
                    return tempPos;
                }
            }
        }
         

        bool NearEnemySearch(Collider[] cols)
        {
            float minDis = float.MaxValue;
            Collider targetCol = null;
            foreach (Collider col in cols)
            {
                float dis = (monster.transform.position - col.transform.position).magnitude;
                if (minDis > dis)
                {
                    minDis = dis;
                    targetCol = col;
                }
            }
            if (targetCol != null)
            {
                monster.targetCol = targetCol;
                return true;
            }
            return false;
        }
    }

    public class MonsterChaseState : MonsterState
    {
        float targetDis;
        const float ATTACK_RANGE = 5f;
        const float MISSING_TARGET_RANGE = 10f;
        public override void OnEnter()
        {
         
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            DetectiveStunPlayer();

            if (monster.targetCol != null)
                targetDis = (monster.transform.position - monster.targetCol.transform.position).magnitude;
            if (monster.targetCol == null || targetDis > MISSING_TARGET_RANGE)
            {
                sm.SetState("Move");
                return;
            }
            if (targetDis < ATTACK_RANGE) // 공격범위안에 들어왔다. 공격상태로 전환
            {                
                sm.SetState("Attack");
            }

            monster.agent.SetDestination(monster.targetCol.transform.position);

            #region rte
            //else
            //{
            //    // TODO_LIST
            //    // 캐싱하기
            //    // 공격 상태 넣기
            //    if ((monsterAI.transform.position - monsterAI.targetCol.transform.position).magnitude < 2f)
            //    {
            //        sm.SetState("Attack");
            //    }
            //    else
            //    {
            //        Vector3 dir = monsterAI.targetCol.transform.position - monsterAI.transform.position;
            //        dir.y = 0;
            //        if (dir != Vector3.zero)
            //            monsterAI.transform.forward = dir;
            //        monsterAI.transform.position = Vector3.MoveTowards(monsterAI.transform.position, monsterAI.targetCol.transform.position, Time.deltaTime * 0.4f);
            //    }                
            //}
            #endregion
        }

        public void DetectiveStunPlayer()
        {
            // 기절한 놈이 범위안에 존재 : targetCol을 기절한 놈으로
        
            // 타겟이 몬스터의 공격범위안에 존재 : 공격상태로 변경

            // 기절한 플레이어 탐지용 OverlapSphere
            Collider[] cols = Physics.OverlapSphere(monster.transform.position, 10f, 1 << 6);

            foreach (Collider col in cols)
            {
                if (col.GetComponent<Player>().IsStun)
                {
                    monster.targetCol = col;
                    break;
                }
            }

            //if (DetectiveStunPlayer(cols)) // 추격상태에서 계속 기절한 놈이 있는지를 찾다보니 찾았다!
            //{
            //    monster.targetCol = 
            //}
        }
    }

    public class MonsterAttackState : MonsterState
    {
        
        float nowTime;
        const float ATK_TIME = 3f;        
        public bool IsDeathAttack
        {
            get => isDeathAttack;
            set
            {
                isDeathAttack = value;
                if(isDeathAttack)
                    monster.AttackState = Monster.ATTACK_STATE.DEATH_ATTACK;
                else
                    monster.AttackState = Monster.ATTACK_STATE.DEFAULT_ATTACK;
            }            
        }
        private bool isDeathAttack;
        public override void Init(IStateMachine sm)
        {            
            base.Init(sm);                        
        }        

        public override void OnEnter()
        {
            // 공격전략이 추가됨에 따라 조건문은 늘어나는 구조임 -> 교수님한테 질문해볼것(코드 훔쳐감-괴도민정-)
            monster.agent.SetDestination(monster.transform.position);             
            IsDeathAttack = monster.targetCol.GetComponent<Player>().IsStun; // 조건 체크 후 Death로
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            nowTime += Time.deltaTime;
            if (nowTime >= ATK_TIME)
            {
                sm.SetState("Move");
                nowTime = 0f;
            }
        }
    }

    public abstract class AttackStrategy
    {                
        public abstract void Attack(Player player); // 대미지를 입을 때
        public abstract void Action(); // 행위

        // 기능 추가
    }

    public abstract class MonsterAttackStrategy : AttackStrategy
    {
        protected Monster monster;
        public MonsterAttackStrategy(Monster monster)
        {
            this.monster = monster;
        }

        public override void Action()
        {
            
        }
    }

    public class DefaultAttackStrategy : MonsterAttackStrategy
    {
        public DefaultAttackStrategy(Monster monster) : base(monster)
        {
        }

        public override void Attack(Player player)
        {
            Debug.Log("애드포스");
            //player.TakeDamaged(10);
            // player.GetComponent<Rigidbody>().add
            // 애드포스 할예정
        }
        
    }

    public class DeathAttackStrategy : MonsterAttackStrategy
    {
        public DeathAttackStrategy(Monster monster) : base(monster)
        {
        }

        public override void Attack(Player player)
        {
            //player.Die();
            //player.TakeDamaged(10);
            // player.GetComponent<Rigidbody>().add
            // 애드포스 할예정
        }        
    }

    public class StateMachine<T> : IStateMachine where T : class
    {
        public T owner = null;
        public State curState = null;

        Dictionary<string, State> stateDic = null;

        public StateMachine()
        {
            stateDic = new Dictionary<string, State>();
        }

        public void AddState(string name, State state)
        {
            if (stateDic.ContainsKey(name))
                return;

            state.Init(this);
            stateDic.Add(name, state);
        }

        public object GetOwner()
        {
            return owner;
        }

        public void SetState(string name)
        {
            if (stateDic.ContainsKey(name))
            {
                if (curState != null)
                    curState.OnExit();
                curState = stateDic[name];
                curState.OnEnter();
            }
        }

        public void Update()
        {
            curState.OnUpdate();
        }

    }

    

    public class Monster : MonoBehaviour
    {
        public enum ATTACK_STATE
        {
            DEFAULT_ATTACK,
            DEATH_ATTACK
        }        

        public Collider targetCol;
        public NavMeshAgent agent;

        public Dictionary<ATTACK_STATE, MonsterAttackStrategy> strategies;        
        public MonsterWeapon monsterWeapon;

        // 
        // MonsterAttackZone이 MonsterAttackStrategy를 가지고 있는다.
        // Monster는 MonsterAttackZone을 가짐.

        private StateMachine<Monster> sm;
        private Animator animator;
        private Dictionary<ATTACK_STATE, string> aniDic;
        private Collider attackCol;

        public ATTACK_STATE AttackState
        {
            get => attackState;
            set
            {
                attackState = value;
                monsterWeapon.AttackStrategy = strategies[value];
                animator.SetTrigger(aniDic[value]);                
            }
        }
        private ATTACK_STATE attackState;


        private void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            sm = new StateMachine<Monster>();            
            attackCol = GetComponentInChildren<CapsuleCollider>();
            sm.owner = this;

            strategies = new Dictionary<ATTACK_STATE, MonsterAttackStrategy>();
            strategies.Add(ATTACK_STATE.DEFAULT_ATTACK, new DefaultAttackStrategy(this));
            strategies.Add(ATTACK_STATE.DEATH_ATTACK, new DeathAttackStrategy(this));

            aniDic = new Dictionary<ATTACK_STATE, string>();
            aniDic.Add(ATTACK_STATE.DEFAULT_ATTACK, "DefaultAttackTrigger");
            aniDic.Add(ATTACK_STATE.DEATH_ATTACK, "DeathAttackTrigger");

            sm.AddState("Move", new MonsterMoveState());
            sm.AddState("Chase", new MonsterChaseState());
            sm.AddState("Attack", new MonsterAttackState());

            sm.SetState("Move"); // 처음 디폴트 상태 Move 세팅
        }

        private void Update()
        {
            sm.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 10);
        }

        public void AttackStart()
        {
            attackCol.enabled = true;            
        }
        public void AttackEnd()
        {
            attackCol.enabled = false;            
        }
    }

}

