//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyStateData
//{
//    public EnemyStateData(NavMeshAgent agent, Transform escape, GameObject me, int hitDamage, float hitPerSec, float disengageDistance, Animator animator)
//    {
//        this.agent = agent;
//        this.escape = escape;
//        this.me = me;
//        this.hitDamage = hitDamage;
//        this.hitPerSec = hitPerSec;
//        this.disengageDistance = disengageDistance;
//        this.animator = animator;
//    }

//    public Animator animator;
//    public NavMeshAgent agent;
//    public Transform escape;
//    public GameObject me;
//    public int hitDamage;
//    public float hitPerSec;
//    public float disengageDistance;

//    public GameObject dmgInstigator;

//}

//public class EnemyState : FSM
//{
//    protected EnemyStateData data;

//    public EnemyState(EnemyStateData data) : base()
//    {
//        this.data = data;
//    }
//}

//public class GotoEscapeState : EnemyState
//{
//    public GotoEscapeState(EnemyStateData data) : base(data)
//    {

//    }

//    protected override void Init()
//    {
//        data.agent.SetDestination(data.escape.position); ;
//        data.agent.isStopped = false;
//        data.animator.SetTrigger("Walk");
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (data.dmgInstigator != null)
//        {
//            base.nextState = new GotoDefenderState(data, data.dmgInstigator);
//            base.currentStep = STEP.EXIT;
//            data.dmgInstigator = null;
//            return;
//        }

//        if (data.agent.pathPending)
//        {
//            // remaining distance is not updated yet, wait a frame.
//            return;
//        }

//        if (data.agent.remainingDistance < 0.2)
//        {
//            LevelManager.Instance.EnemyEscaped(data.me);
//            GameObject.Destroy(data.me);
//            return;
//        }

//        if (data.agent.velocity.magnitude < data.agent.speed / 3f)
//        {
//            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Defender");
//            float minDist = 2;
//            GameObject closestEnemy = null;
//            foreach (GameObject enemy in enemies)
//            {
//                float dist = Vector2.Distance(enemy.transform.position, data.me.transform.position);
//                if (dist < minDist)
//                {
//                    closestEnemy = enemy;
//                }
//            }
//            if (closestEnemy != null)
//            {
//                base.nextState = new GotoDefenderState(data, closestEnemy);
//                base.currentStep = STEP.EXIT;
//                return;
//            }
//        }

//        // Update destination to handle navmesh bloker
//        data.agent.SetDestination(data.escape.position);
//    }

//    protected override void Exit()
//    {
//        data.animator.ResetTrigger("Walk");
//        base.Exit();
//    }
//}

//public class GotoDefenderState : EnemyState
//{
//    GameObject defender;
//    public GotoDefenderState(EnemyStateData data, GameObject defender) : base(data)
//    {
//        this.defender = defender;
//    }

//    protected override void Init()
//    {
//        data.agent.SetDestination(defender.transform.position);
//        data.agent.isStopped = false;
//        data.animator.SetTrigger("Walk");
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (defender == null)
//        {
//            // enemy die, go to escape
//            base.nextState = new GotoEscapeState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        if (data.agent.pathPending)
//        {
//            // remaining distance is not updated yet, wait a frame.
//            return;
//        }

//        if (Vector2.Distance(data.me.transform.position, defender.transform.position) < 1)
//        {
//            base.nextState = new AttackDefenderState(data, defender);
//            base.currentStep = STEP.EXIT;
//        }

//        // FIXME: if too slow, search a closest unit?
//    }

//    protected override void Exit()
//    {
//        data.animator.ResetTrigger("Walk");
//        base.Exit();
//    }
//}
//public class AttackDefenderState : EnemyState
//{
//    GameObject target;

//    float LastAttack = 0;

//    public AttackDefenderState(EnemyStateData data, GameObject target) : base(data)
//    {
//        this.target = target;
//    }

//    protected override void Init()
//    {
//        data.agent.isStopped = true;
//        data.animator.SetTrigger("Fight");
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if (target == null)
//        {
//            // Enemy died
//            base.nextState = new GotoEscapeState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        if (Time.time - LastAttack > 1 / data.hitPerSec)
//        {
//            // Put it in canAttack to avoid having a higher "dps" than expected
//            if (Vector2.Distance(target.transform.position, data.me.transform.position) > data.disengageDistance)
//            {
//                base.nextState = new GotoDefenderState(data, target);
//                base.currentStep = STEP.EXIT;
//                return;
//            }

//            target.GetComponent<Damageable>().DealDamage(data.hitDamage, data.me);
//            LastAttack = Time.time;
//        }
//    }
//    protected override void Exit()
//    {
//        data.animator.ResetTrigger("Fight");
//        base.Exit();
//    }
//}

//public class EnemyBehavior : MonoBehaviour
//{

//    [Header("AI data")]
//    [SerializeField] private Transform target;
//    [SerializeField] private int hitDamage = 5;
//    [SerializeField] private float hitPerSec = 2;
//    [SerializeField] private float disengageDistance = 2;

//    private EnemyState state = null;
//    private NavMeshAgent agent;
//    bool isMovingRight = true;

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.tag == "BigProjectile")
//        {
//            GetComponent<Damageable>().DealDamage(collision.gameObject.GetComponent<ProjectileBehavior>().numDamage, collision.gameObject);
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false;
//        agent.updateUpAxis = false;

//        EnemyStateData data = new EnemyStateData(agent, target, gameObject, hitDamage, hitPerSec, disengageDistance, GetComponent<Animator>());
//        state = new GotoEscapeState(data);

//        GetComponent<Damageable>().OnHit += (GameObject damageable, GameObject instigator) => data.dmgInstigator = instigator;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        state = (EnemyState)state.Progress();

//        if (isMovingRight && agent.velocity.x < 0)
//        {
//            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
//            isMovingRight = false;
//        }
//        else if (!isMovingRight && agent.velocity.x > 0)
//        {
//            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
//            isMovingRight = true;
//        }
//    }
//}
