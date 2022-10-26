using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponUpgradeData", order = 1)]
public class WeaponUpgradeData : ScriptableObject
{
    public string weaponName;
    public int cost;
    public MagicBall magicBall;
    public List<WeaponUpgradeData> requirements;
}