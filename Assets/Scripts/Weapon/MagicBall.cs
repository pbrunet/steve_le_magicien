using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int damagePerSec;
    [SerializeField] private float damageDuration;

    public float DamageDuration { get { return damageDuration; } }
    public int Damage { get { return damage; } }
    public int numDamage { get { return damagePerSec; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
