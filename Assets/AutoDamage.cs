using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDamage : MonoBehaviour
{
    public int dmgPerSec;

    private Damageable m_dmg;

    // Start is called before the first frame update
    void Start()
    {
        m_dmg = GetComponent<Damageable>();
        InvokeRepeating("Hit", 0, 1);
    }

    // Update is called once per frame
    void Hit()
    {
        m_dmg.DealDamage(dmgPerSec, gameObject);
    }
}
