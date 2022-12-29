using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackKind
{
    SHOT_BULLET,
    SHOT_METEOR
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponUpgradeData", order = 1)]
public class WeaponUpgradeData : ScriptableObject
{
    public Sprite weaponImg;
    public string weaponName;
    public int cost;
    public MagicBall magicBall;
    public AttackKind kind;
    public List<WeaponUpgradeData> requirements;
}