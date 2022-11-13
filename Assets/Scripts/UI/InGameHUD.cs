using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private TextMeshProUGUI beer;
    [SerializeField] private TextMeshProUGUI garbage;
    [SerializeField] private TextMeshProUGUI weapon;

    public void UpdadeLifeGUI()
    {
        life.SetText("Life : " + PlayerData.Instance.Life);
    }

    public void UpdadeBeerGUI()
    {
        beer.SetText("Beer : " + PlayerData.Instance.Beer);
    }

    public void UpdateGarbageGUI()
    {
        garbage.SetText("Garbage : " + PlayerData.Instance.Garbage);
    }

    public void UpdateWeaponGUI()
    {
        string ownedWeapons = "";
        bool first = true;
        foreach(WeaponUpgradeData weapon in PlayerData.Instance.BuyWeapons)
        {
            if(!first)
            {
                ownedWeapons += ", ";
            }
            first = false;
            ownedWeapons += weapon.weaponName;
        }

        weapon.SetText(System.String.Format("Equiped Weapon : {0}\nOwned Weapon : [{1}]", PlayerData.Instance.EquipedWeapon.weaponName, ownedWeapons));
    }

    private void Start()
    {
        UpdadeBeerGUI();
        UpdateGarbageGUI();
        UpdadeLifeGUI();
        UpdateWeaponGUI();
    }
}
