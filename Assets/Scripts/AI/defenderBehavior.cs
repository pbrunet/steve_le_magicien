//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class DefenderStateData
//{
//    public DefenderStateData(NavMeshAgent agent, GameObject me, Vector3 spawnPos, SpawnDefender spawner, float detectRange, float hitPerSec, float engageDistance, float disengageDistance, int hitDamage)
//    {
//        this.agent = agent;
//        this.me = me;
//        this.spawnPos = spawnPos;
//        this.spawner = spawner;
//        this.detectRange = detectRange;
//        this.hitPerSec = hitPerSec;
//        this.engageDistance = engageDistance;
//        this.disengageDistance = disengageDistance;
//        this.hitDamage = hitDamage;
//    }

//    public NavMeshAgent agent;
//    public GameObject me;
//    public Vector3 spawnPos;
//    public SpawnDefender spawner;
//    public float detectRange;
//    public float hitPerSec;
//    public float engageDistance;
//    public float disengageDistance;
//    public int hitDamage;
//}

//public class DefenderState : FSM
//{
//    protected DefenderStateData data;

//    public DefenderState(DefenderStateData data) : base()
//    {
//        this.data = data;
//    }

//    protected GameObject SearchClosestEnemy()
//    {
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        float minDist = float.PositiveInfinity;
//        GameObject closestEnemy = null;
//        foreach (GameObject enemy in enemies)
//        {
//            float dist = Vector2.Distance(enemy.transform.position, data.me.transform.position);
//            if (dist < minDist && dist < data.detectRange && Vector2.Distance(enemy.transform.position, data.spawner.transform.position) < data.spawner.range)
//            {
//                closestEnemy = enemy;
//            }
//        }
//        return closestEnemy;
//    }
//}

//public class DefenderIdleState : DefenderState
//{
//    public DefenderIdleState(DefenderStateData data) : base(data)
//    {
//    }

//    protected override void Init() { base.Init(); }
//    protected override void Update()
//    {
//        GameObject enemy = SearchClosestEnemy();
//        if(enemy != null)
//        {
//            base.nextState = new GotoEnemyState(data, enemy);
//            base.currentStep = STEP.EXIT;
//        }
//    }
//    protected override void Exit() { base.Exit(); }
//}
//public class GotoEnemyState : DefenderState
//{
//    GameObject target;

//    public GotoEnemyState(DefenderStateData data, GameObject target) : base(data)
//    {
//        this.target = target;
//    }

//    protected override void Init()
//    {
//        data.agent.isStopped = false;
//        data.agent.SetDestination(target.transform.position);
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (target == null)
//        {
//            // Enemy died
//            base.nextState = new GotoSpawnState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        data.agent.SetDestination(target.transform.position);

//        if (Vector2.Distance(target.transform.position, data.me.transform.position) < data.engageDistance)
//        {
//            base.nextState = new AttackEnemyState(data, target);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        if (Vector2.Distance(target.transform.position, data.spawner.transform.position) >= data.spawner.range)
//        {
//            base.nextState = new GotoSpawnState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }
//    }
//    protected override void Exit() { base.Exit(); }
//}
//public class AttackEnemyState : DefenderState
//{
//    GameObject target;

//    float LastAttack = 0;

//    public AttackEnemyState(DefenderStateData data, GameObject target) : base(data)
//    {
//        this.target = target;
//    }

//    protected override void Init()
//    {
//        data.agent.isStopped = true;
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (target == null)
//        {
//            // Enemy died
//            base.nextState = new GotoSpawnState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        if (Time.time - LastAttack > 1 / data.hitPerSec)
//        {
//            // Put it in canAttack to avoid having a higher "dps" than expected
//            if (Vector2.Distance(target.transform.position, data.me.transform.position) > data.disengageDistance)
//            {
//                base.nextState = new GotoEnemyState(data, target);
//                base.currentStep = STEP.EXIT;
//                return;
//            }

//            target.GetComponent<Damageable>().DealDamage(data.hitDamage, data.me);
//            LastAttack = Time.time;
//        }
//    }
//    protected override void Exit() { base.Exit(); }
//}
//public class GotoSpawnState : DefenderState
//{
//    public GotoSpawnState(DefenderStateData data) : base(data)
//    {
//    }

//    protected override void Init()
//    {
//        data.agent.isStopped = false;
//        data.agent.SetDestination(data.spawnPos);
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (data.agent.pathPending)
//        {
//            // remaining distance is not updated yet, wait a frame.
//            return;
//        }

//        GameObject enemy = SearchClosestEnemy();
//        if (enemy != null)
//        {
//            base.nextState = new GotoEnemyState(data, enemy);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        data.agent.SetDestination(data.spawnPos);
//        if (data.agent.remainingDistance <= data.agent.stoppingDistance)
//        {
//            base.nextState = new DefenderIdleState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }
//    }
//    protected override void Exit() { base.Exit(); }
//}

//public class defenderBehavior : MonoBehaviour
//{
//    private DefenderState state;
//    public SpawnDefender spawner;
//    public float detectRange = 5;
//    public float hitPerSec = 3;
//    public int hitDamage = 7;
//    public float engageDistance = 1;
//    public float disengageDistance = 2;

//    private void Start()
//    {
//        NavMeshAgent agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false;
//        agent.updateUpAxis = false;

//        // FIXME: transform is not already on the navmesh.
//        DefenderStateData data = new DefenderStateData(agent, gameObject, transform.position, spawner, detectRange, hitPerSec, engageDistance, disengageDistance, hitDamage);

//        state = new DefenderIdleState(data);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        state = (DefenderState)state.Progress();
//    }
//}
