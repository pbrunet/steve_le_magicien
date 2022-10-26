//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class TowerStateData
//{
//    public TowerStateData(GameObject projectile, GameObject me, float hitPerSec, float detectRange)
//    {
//        this.projectile = projectile;
//        this.me = me;
//        this.hitPerSec = hitPerSec;
//        this.detectRange = detectRange;
//    }

//    public GameObject projectile;
//    public GameObject me;
//    public float hitPerSec;
//    public float detectRange;

//}

//public class TowerState : FSM
//{
//    protected TowerStateData data;

//    public TowerState(TowerStateData data) : base()
//    {
//        this.data = data;
//    }
//}

//public class SearchEnemyState : TowerState
//{
//    public SearchEnemyState(TowerStateData data) : base(data)
//    {

//    }

//    protected override void Init()
//    {
//        base.Init();
//    }
//    protected override void Update()
//    {
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        foreach (GameObject enemy in enemies)
//        {
//            if (Vector2.Distance(enemy.transform.position, data.me.transform.position) < data.detectRange)
//            {
//                base.nextState = new ShootAtEnemyState(data, enemy);
//                base.currentStep = STEP.EXIT;
//                break;
//            }
//        }
//    }

//    protected override void Exit() { base.Exit(); }
//}

//public class ShootAtEnemyState : TowerState
//{
//    private GameObject enemy;
//    private float LastAttack = 0;

//    public ShootAtEnemyState(TowerStateData data, GameObject enemy) : base(data)
//    {
//        this.enemy = enemy;
//    }

//    protected override void Init()
//    {
//        base.Init();
//    }
//    protected override void Update()
//    {
//        if(enemy == null)
//        {
//            base.nextState = new SearchEnemyState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        float distance = Vector2.Distance(enemy.transform.position, data.me.transform.position);
//        if (distance > data.detectRange)
//        {
//            base.nextState = new SearchEnemyState(data);
//            base.currentStep = STEP.EXIT;
//            return;
//        }

//        if (Time.time - LastAttack > 1 / data.hitPerSec)
//        {

//            GameObject proj = GameObject.Instantiate(data.projectile);
//            proj.transform.position = data.me.transform.position;
//            var agent = enemy.GetComponent<NavMeshAgent>();
//            proj.GetComponent<MoveProjectile>().target = agent;

//            // FIXME: we may have "advance" a little to much thus we should not set to 0 the timer to avoid different behavior depending on fps
//            LastAttack = Time.time;
//        }
//    }

//    protected override void Exit() { base.Exit(); }
//}

//public class ShootAt : MonoBehaviour
//{
//    [SerializeField] private GameObject projectile;
//    [SerializeField] private float range;
//    [SerializeField] private float ShotsPerSeconds;
//    public float Range { get { return range; } }

//    private TowerState state;

//    // Start is called before the first frame update
//    void Start()
//    {
//        TowerStateData data = new TowerStateData(projectile, gameObject, ShotsPerSeconds, range);
//        state = new SearchEnemyState(data);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        state = (TowerState)state.Progress();
//    }
//}
