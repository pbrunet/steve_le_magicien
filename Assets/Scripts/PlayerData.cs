using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [SerializeField] int beer;
    [SerializeField] int garbage;
    [SerializeField] List<string> unlockedWeapons = new List<string>();
    [SerializeField] List<string> buyWeapons = new List<string>();
    [SerializeField] string equipedWeapon;
    public int Beer { get { return beer; } }
    public List<string> UnlockedWeapons { get { return unlockedWeapons; } }
    public List<string> BuyWeapons { get { return buyWeapons; } }
    public int Garbage { get { return garbage; } }

    // Start is called before the first frame update
    private void Start()
    {
        beer = PlayerPrefs.GetInt("beer", 0);
        garbage = PlayerPrefs.GetInt("garbage", 0);
        equipedWeapon = PlayerPrefs.GetString("equipedWeapon", "unknown");
        foreach (WeaponUpgradeData weapon in WeaponUpgradeManager.Instance.AllWeapons) {
            int weaponState = PlayerPrefs.GetInt(weapon.weaponName, 0);
            if (weaponState > 0)
            {
                unlockedWeapons.Add(weapon.weaponName);
            }
            if (weaponState > 1)
            {
                buyWeapons.Add(weapon.weaponName);
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("beer", beer);
        PlayerPrefs.SetInt("garbage", garbage);
        PlayerPrefs.SetString("equipedWeapon", equipedWeapon);
        foreach (WeaponUpgradeData weapon in WeaponUpgradeManager.Instance.AllWeapons)
        {
            PlayerPrefs.SetInt(weapon.weaponName, (unlockedWeapons.Contains(weapon.weaponName) ? 1 : 0) + (buyWeapons.Contains(weapon.weaponName) ? 1 : 0));
        }
        PlayerPrefs.Save();
    }

    public string GetCurrentWeapon() { return equipedWeapon; }

    public void loot(int beer, int garbage)
    {
        this.beer += beer;
        this.garbage += garbage;
        Save();
        UIManager.Instance.inGameHUD.UpdadeBeerGUI();
        UIManager.Instance.inGameHUD.UpdateGarbageGUI();
    }
}
