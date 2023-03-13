using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy1State : FSM
{
    protected GameObject gameObject;

    public Enemy1State(GameObject gameObject) : base()
    {
        this.gameObject = gameObject;
    }
}

public class Enemy1StateIdle : Enemy1State
{
    public Enemy1StateIdle(GameObject gameObject) : base(gameObject)
    {
    }

    protected override void Update()
    {
        if(gameObject.GetComponent<Patrol>() != null)
        {
            base.nextState = new Enemy1StatePatrol(gameObject);
        }
    }
}
public class Enemy1StatePatrol : Enemy1State
{
    private Patrol pat;
    private Rigidbody2D rb;
    int nextWaypoint;
    public Enemy1StatePatrol(GameObject gameObject) : base(gameObject)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pat = gameObject.GetComponent<Patrol>();
        nextWaypoint = 0;
        float minDist = Vector2.Distance(pat.waypoints[0], gameObject.transform.position);
        for(int i=1; i<pat.waypoints.Count; i++)
        {
            float dist = Vector2.Distance(pat.waypoints[i], gameObject.transform.position);
            if(dist < minDist)
            {
                minDist = dist;
                nextWaypoint = i;
            }
        }
    }

    protected override void Update()
    {
        if (pat.waypoints[nextWaypoint].x > gameObject.transform.position.x)
        {
            // move left
            rb.velocity = new Vector2(pat.xSpeed, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(-pat.xSpeed, rb.velocity.y);
            // move right
        }
        float dist = Vector2.Distance(pat.waypoints[nextWaypoint], gameObject.transform.position);

        if (dist < pat.distTolerance)
        {
            nextWaypoint = (nextWaypoint + 1) % pat.waypoints.Count;
        }

        
        float distToPlayer = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, gameObject.transform.position);
        if (gameObject.GetComponent<Stalk>() != null && distToPlayer < gameObject.GetComponent<Stalk>().startDistance)
        {
            base.nextState = new Enemy1StateStalk(gameObject, GameObject.FindGameObjectWithTag("Player"));
        }
    }
}

public class Enemy1StateStalk : Enemy1State
{
    private Stalk stalk;
    private Rigidbody2D rb;
    private GameObject target;

    public Enemy1StateStalk(GameObject gameObject, GameObject target) : base(gameObject)
    {
        this.target = target;
        rb = gameObject.GetComponent<Rigidbody2D>();
        stalk = gameObject.GetComponent<Stalk>();
    }

    protected override void Update()
    {
        if (target.transform.position.x > gameObject.transform.position.x)
        {
            // move left
            rb.velocity = new Vector2(stalk.xSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-stalk.xSpeed, rb.velocity.y);
            // move right
        }

        if (Vector2.Distance(target.transform.position, rb.position) > stalk.maxDistance)
        {
            base.nextState = new Enemy1StateIdle(gameObject);
        }

        if (gameObject.GetComponent<EnemyAttack>() != null && Vector2.Distance(target.transform.position, rb.position) < 10)
        {
            base.nextState = new Enemy1StateAttackCAC(gameObject, target);
        }

        if (gameObject.GetComponent<EnemyExplode>() != null && Vector2.Distance(target.transform.position, rb.position) < gameObject.GetComponent<EnemyExplode>().explosionRange)
        {
            target.GetComponent<Damageable>().DealDamage(gameObject.GetComponent<EnemyExplode>().damageOnExplode, gameObject);
            gameObject.GetComponent<Damageable>().DealDamage(gameObject.GetComponent<Damageable>().Life, gameObject);
        }
    }

}
public class Enemy1StateAttackCAC : Enemy1State
{
    private EnemyAttack att;
    private GameObject target;

    public Enemy1StateAttackCAC(GameObject gameObject, GameObject target) : base(gameObject)
    {
        this.target = target;
        att = gameObject.GetComponent<EnemyAttack>();
        gameObject.GetComponent<Enemy1_StateMachine>().StartCoroutine(Attack());
    }

    protected IEnumerator Attack()
    {
        while (Vector2.Distance(target.transform.position, gameObject.transform.position) < 10)
        {
            target.GetComponent<Damageable>().DealDamage(10, gameObject);
            yield return new WaitForSeconds(1);
        }
        base.nextState = new Enemy1StateIdle(gameObject);
    }

}

public class Enemy1_StateMachine : MonoBehaviour
{
    private Enemy1State state;

    private void Start()
    {
        state = new Enemy1StateIdle(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        state = (Enemy1State)state.Progress();
    }
}
