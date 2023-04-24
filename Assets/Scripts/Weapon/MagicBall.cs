using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] public int speed;
    [SerializeField] private int damage;
    [SerializeField] private int damagePerSec;
    [SerializeField] private float damageDuration;
    [SerializeField] private float criticProbability = 0;
    [SerializeField] private float criticMultiplier = 0;

    public float DamageDuration { get { return damageDuration; } }
    public int Damage { get { if (UnityEngine.Random.Range(0f, 1f) < criticProbability) { return (int)(damage * (1 + criticMultiplier)); } else { return damage; } } }
    public int numDamage { get { return damagePerSec; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
