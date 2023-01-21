using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int maxLife = 30;
    [SerializeField] private GameObject greenBarOrigin;

    public event System.Action<GameObject> OnKilled;

    // damage / instigator
    public event System.Action<GameObject, GameObject> OnHit;

    private int life;
    public int Life { get { return life; } }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "projectile")
        {
            MagicBall mb = collision.collider.GetComponent<MagicBall>();
            DealDamage(mb.Damage, collision.collider.gameObject);
            StartCoroutine(doTimedDamaged(mb.numDamage, mb.DamageDuration, collision.collider.gameObject));
        }    
    }

    IEnumerator doTimedDamaged(int numDamage, float duration, GameObject instigator)
    {
        float timeBetweenDamage = 0.1f; // 1 sec between each timed damage
        int dmgDone = 0;
        while(dmgDone < numDamage)
        {
            yield return new WaitForSeconds(timeBetweenDamage);
            int dmg = Mathf.CeilToInt(numDamage * timeBetweenDamage / duration);
            dmgDone += dmg;
            if(dmgDone > numDamage)
            {
                dmg -= numDamage - dmgDone;
            }
            DealDamage(dmg, instigator);
        }
    }

    public void DealDamage(int numDamage, GameObject instigator)
    {
        life -= numDamage;
        if(tag == "Player")
        {
            life = PlayerData.Instance.DamageBy(numDamage);
        }

        if(OnHit != null)
        {
            OnHit(gameObject, instigator);
        }

        if (greenBarOrigin)
        {
            // Update healthBar
            greenBarOrigin.transform.localScale = new Vector3((float)life / maxLife, 1.0f, 1.0f);
        }

        if (life <= 0)
        {
            if (OnKilled != null)
            {
                OnKilled(instigator);
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }
}
