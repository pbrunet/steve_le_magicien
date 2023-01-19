using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameHUD : MonoBehaviour
{
    [System.Serializable]
    public class HUDEntry
    {
        public TextMeshProUGUI txt;
        public Image img;
    }
    [SerializeField] private HUDEntry life;
    [SerializeField] private HUDEntry beer;
    [SerializeField] private HUDEntry garbage;
    [SerializeField] private HUDEntry ghost;
    [SerializeField] private HUDEntry weapon;

    public void UpdadeLifeGUI()
    {
        life.txt.SetText("Life : " + PlayerData.Instance.Life);
    }

    public void UpdadeBeerGUI()
    {
        beer.txt.SetText(PlayerData.Instance.Beer.ToString());
    }

    public void UpdateGarbageGUI()
    {
        garbage.txt.SetText(PlayerData.Instance.Garbage.ToString());
    }
    public void UpdateGhostGUI()
    {
        ghost.txt.SetText(PlayerData.Instance.Ghost.ToString());
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

        weapon.img.sprite = PlayerData.Instance.EquipedWeapon.weaponImg;

        weapon.txt.SetText(System.String.Format("Equiped Weapon : {0}\nOwned Weapon : [{1}]", PlayerData.Instance.EquipedWeapon.weaponName, ownedWeapons));
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
