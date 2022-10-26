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
            StartCoroutine(doTimedDamaged(mb.DamagePerSec, mb.DamageDuration, collision.collider.gameObject));
        }    
    }

    IEnumerator doTimedDamaged(int damagePerSec, float duration, GameObject instigator)
    {
        float timeBetweenDamage = 1; // 1 sec between each timed damage
        int numRepeat = (int)(duration / timeBetweenDamage);
        for(int i=0; i<numRepeat; i++)
        {
            yield return new WaitForSeconds(timeBetweenDamage);
            DealDamage(damagePerSec, instigator);
        }
    }

    public void DealDamage(int numDamage, GameObject instigator)
    {
        life -= numDamage;
        if(tag == "Player")
        {
            UIManager.Instance.inGameHUD.UpdadeLifeGUI(this);
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
        if (tag == "Player")
        {
            UIManager.Instance.inGameHUD.UpdadeLifeGUI(this);
        }
    }
}
