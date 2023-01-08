using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private int Beer;
    [SerializeField] private int Garbage;

    private void Start() {
        GetComponent<Damageable>().OnKilled += Loot_OnKilled;
    }

    private void Loot_OnKilled(GameObject instigator)
    {
        PlayerData.Instance.loot(Beer, Garbage, 1);
    }
}
