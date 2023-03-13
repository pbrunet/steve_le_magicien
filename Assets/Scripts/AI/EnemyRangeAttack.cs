using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{

    public MagicBall projectile;
    public float attackDistance = 100;
    public float projectileSpeed = 5;
    public float reloadTime = 1;

    private float lastAttackTime = 0;

    public bool IsReady()
    {
        if(Time.time < lastAttackTime + reloadTime)
        {
            return false;
        }

        float distToPlayer = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, gameObject.transform.position);
        return distToPlayer < attackDistance;
    }
    public void Attack()
    {
        lastAttackTime = Time.time;
        float dir = 1;
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x < gameObject.transform.position.x)
        {
            dir = -1;
        }
        MagicBall bullet = Instantiate<MagicBall>(projectile, new Vector3(gameObject.transform.position.x + 5 * dir, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed * dir, 0, 0);
    }
}
