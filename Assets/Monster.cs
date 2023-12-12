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
            Debug.Log("테스트");
            base.Init(sm);
            monster = (Monster)sm.GetOwner();
        }                
    }

    public class MonsterMoveState : MonsterState
    {
        const float RANGE_DIS = 2f;
        public override void OnEnter()
        {
            monster.targetCol = null;
            targetPos = SetRandPos();
           
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
            Debug.Log("코드상 이전 위치" + targetPos);

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
                targetPos = SetRandPos();
            }

        }

        public Vector3 SetRandPos()
        {
            Vector3 tempPos;
            while (true)
            {                
                float randX = Random.Range(0f, 30f);
                float randZ = Random.Range(0f, 30f);
                Debug.Log(randX + " / " + randZ);
                tempPos = new Vector3(randX, 10, randZ);
                
                Debug.Log($"{tempPos.y} / {monster.transform.position.y}");
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
                // sm.SetState("Attack");
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
                if (col.GetComponent<Player>().isStun)
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
        public override void OnEnter()
        {
            Debug.Log("공격상태로 돌입");
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

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
            Debug.Log("asdas");
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
        StateMachine<Monster> sm;
        public Collider targetCol;
        public NavMeshAgent agent;
        


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            sm = new StateMachine<Monster>();
            sm.owner = this;

            sm.AddState("Move", new MonsterMoveState());
            sm.AddState("Chase", new MonsterChaseState());
            sm.AddState("Attack", new MonsterAttackState());

            sm.SetState("Move"); // 처음 디폴트 상태 Move 세팅
        }

        private void Update()
        {
            sm.Update();
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, 10);
        //}
    }

}
