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
    [SerializeField] private TextMeshProUGUI ghost;
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
        garbage.SetText(PlayerData.Instance.Garbage.ToString());
    }
    public void UpdateGhostGUI()
    {
        ghost.SetText(PlayerData.Instance.Ghost.ToString());
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

        weapon.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = PlayerData.Instance.EquipedWeapon.weaponImg;

        weapon.SetText(System.String.Format("Equiped Weapon : {0}\nOwned Weapon : [{1}]", PlayerData.Instance.EquipedWeapon.weaponName, ownedWeapons));
    }

    private void Start()
    {
        UpdadeBeerGUI();
        UpdateGarbageGUI();
        UpdateGhostGUI();
        UpdadeLifeGUI();
        UpdateWeaponGUI();
    }

}
