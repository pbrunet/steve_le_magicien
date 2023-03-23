using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackKind
{
    SHOT_BULLET,
    SHOT_METEOR,
    INVOKE,
    SNIPE,
    SHURIKEN

}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponUpgradeData", order = 1)]
public class WeaponUpgradeData : ScriptableObject
{
    public Sprite weaponImg;
    public string weaponName;
    public int cost;
    public GameObject magicBall;
    public AttackKind kind;
    public float reloadDelay = 1f;
    public int maxProjectiles = 3;
    public List<WeaponUpgradeData> requirements;
}