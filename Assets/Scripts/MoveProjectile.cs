using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveProjectile : MonoBehaviour
{
   // private float g = 9.1f;

    public float height = 5;
    public float peak = 10;
    public float speed = 10;
    public NavMeshAgent target;

    private float totalTime;
    private float currentTime = 0;
    private float vz;
    private float g;
    private Vector2 initPos;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        // see https://www.forrestthewoods.com/blog/solving_ballistic_trajectories/

        Vector2 dist = target.gameObject.transform.position - transform.position;
        dir = dist;
        float a = ((Vector2)target.velocity).sqrMagnitude - speed * speed;
        float b = Vector2.Dot(dist, ((Vector2)target.velocity));
        float c = dist.sqrMagnitude;

        float delta = b * b - 4 * a * c;
        if(delta < 0)
        {
            Destroy(gameObject);
            Debug.LogError("We should not fire an arrov to 'too far' target");
        }
        float lowT = (-b - Mathf.Sqrt(delta)) / (2 * a);

        // As tower is higher than target
        //float highT = (-b + Mathf.Sqrt(delta)) / (2 * a);

        totalTime = lowT;
        float targetHeight = 0;

        g = 4 * (height - 2 * peak + targetHeight) / (totalTime * totalTime);
        vz = -(3 * height - 4 * peak + targetHeight) / totalTime;

        transform.right = dist.normalized;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        Vector2 dist = new Vector2(dir.normalized.x * speed, speed * dir.normalized.y + vz + g * currentTime);
        transform.right = dist.normalized;

        float z = height + currentTime * vz + g * currentTime * currentTime / 2;
        // FIXME: Should we disable collision until z is <= 1?

        transform.position = new Vector3(initPos.x + speed * dir.normalized.x * currentTime, initPos.y + speed * dir.normalized.y * currentTime + z, 0);
        if(totalTime <= currentTime)
        {
            target.gameObject.GetComponent<Damageable>().DealDamage(gameObject.GetComponent<ProjectileBehavior>().numDamage, gameObject);
            Destroy(gameObject);
        }
    }
}
