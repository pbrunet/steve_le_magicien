using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class WeaponUpgradeManager : Singleton<WeaponUpgradeManager>
{
    [SerializeField] private List<WeaponUpgradeData> allWeapons;
    [SerializeField] private WeaponUpgradeData defaultWeapon;

    public List<WeaponUpgradeData> AllWeapons { get { return allWeapons; } }
    public WeaponUpgradeData DefaultWeapon { get { return defaultWeapon; } }
}
