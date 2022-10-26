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

    public List<List<WeaponUpgradeData>> GetWeaponPerLevel()
    {
        List<List<WeaponUpgradeData>> res = new List<List<WeaponUpgradeData>>();
        res.Add(new List<WeaponUpgradeData>());
        foreach (WeaponUpgradeData weapon in allWeapons)
        {
            if (weapon.requirements.Count == 0)
            {
                res[0].Add(weapon);
            }
        }

        int currentLevel = 1;
        while(true)
        {

            foreach (WeaponUpgradeData weaponBase in res[currentLevel - 1]) {
                foreach (WeaponUpgradeData weapon in allWeapons)
                {
                    if (weapon.requirements.Contains(weaponBase))
                    {
                        if (res.Count == currentLevel)
                        {
                            res.Add(new List<WeaponUpgradeData>());
                        }
                        res[currentLevel].Add(weapon);

                    }
                }
            }
            if (res.Count == currentLevel)
            {
                break;
            }
            currentLevel++;
        }

        return res;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
