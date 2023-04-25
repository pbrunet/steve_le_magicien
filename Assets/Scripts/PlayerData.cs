using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{

    static public int VERSION = 1;

    [Header("Player")]
    [SerializeField] int initLife = 100;
    private int life;
    public int InitLife { get { return initLife; } }
    public int Life { get { return life; } }

    [Header("Money")]
    [SerializeField] int initBeer;
    private int beer;
    public int Beer { get { return beer; } }

    [SerializeField] int initGarbage;
    private int garbage;
    public int Garbage { get { return garbage; } }

    [SerializeField] int initGhost;
    private int ghost;
    public int Ghost { get { return ghost; } }

    [Header("Weapon")]
    [SerializeField] WeaponUpgradeData defaultWeapon;
    private WeaponUpgradeData equipedWeapon;
    public WeaponUpgradeData EquipedWeapon { get { return equipedWeapon; } }

    private List<WeaponUpgradeData> unlockedWeapons = new List<WeaponUpgradeData>();
    public List<WeaponUpgradeData> UnlockedWeapons { get { return unlockedWeapons; } }

    private List<WeaponUpgradeData> buyWeapons = new List<WeaponUpgradeData>();
    public List<WeaponUpgradeData> BuyWeapons { get { return buyWeapons; } }

    [Header("PlayerUpgrade")]
    private Dictionary<PlayerUpgradeId, int> playerUpgradeLevel  = new Dictionary<PlayerUpgradeId, int>();

    // Start is called before the first frame update
    private void Start()
    {
        int version = PlayerPrefs.GetInt("version", VERSION - 1);
        if(version != VERSION)
        {
            PlayerPrefs.DeleteAll();
        }

        beer = PlayerPrefs.GetInt("beer", initBeer);
        garbage = PlayerPrefs.GetInt("garbage", initGarbage);
        ghost = PlayerPrefs.GetInt("ghost", initGhost);
        life = PlayerPrefs.GetInt("life", initLife);

        // Serialize weapon information
        foreach (WeaponUpgradeData weapon in WeaponUpgradeManager.Instance.AllWeapons) {
            int weaponState = PlayerPrefs.GetInt(weapon.weaponName, 0);
            if (Convert.ToBoolean(weaponState & 1))
            {
                unlockedWeapons.Add(weapon);
            }
            if (Convert.ToBoolean(weaponState & 2))
            {
                buyWeapons.Add(weapon);
            }
            if (Convert.ToBoolean(weaponState & 4))
            {
                equipedWeapon = weapon;
            }
        }

        if(equipedWeapon is null)
        {
            equipedWeapon = defaultWeapon;
            buyWeapons.Add(defaultWeapon);
            unlockedWeapons.Add(defaultWeapon);
        }

        // Serialize Player upgrade information
        foreach (PlayerUpgradeData upgrade in PlayerUpgrade.Instance.AllUpgrades)
        {
            int upgradeLevel = PlayerPrefs.GetInt("PlayerUpgrade" + upgrade.kind, 0);
            playerUpgradeLevel.Add(upgrade.kind, upgradeLevel);
        }
    }

    public int DamageBy(int _life)
    {
        this.life -= _life;
        if(this.life <= 0)
        {
            life = initLife;
            GameManager.Instance.Restart();
        }
        UIManager.Instance.inGameHUD.UpdadeLifeGUI();
        return this.life;
    }
    public int HealBy(int _life)
    {
        this.life += _life;
        UIManager.Instance.inGameHUD.UpdadeLifeGUI();
        return this.life;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("version", VERSION);
        PlayerPrefs.SetInt("beer", beer);
        PlayerPrefs.SetInt("garbage", garbage);
        PlayerPrefs.SetInt("ghost", ghost);
        PlayerPrefs.SetInt("life", life);
        foreach (WeaponUpgradeData weapon in WeaponUpgradeManager.Instance.AllWeapons)
        {
            PlayerPrefs.SetInt(weapon.weaponName, (unlockedWeapons.Contains(weapon) ? 1 : 0) | ((buyWeapons.Contains(weapon) ? 1 : 0) << 1) | (((weapon is WeaponUpgradeData equipedWeapon) ? 1 : 0) << 2));
        }
        foreach (KeyValuePair<PlayerUpgradeId, int> element in playerUpgradeLevel)
        {
            PlayerPrefs.SetInt("PlayerUpgrade" + element.Key, element.Value);
        }
        PlayerPrefs.Save();
    }

    public WeaponUpgradeData GetCurrentWeapon() { return equipedWeapon; }
    public void GetNextWeapon() {

        for(int i=0; i< buyWeapons.Count; i++)
        {
            WeaponUpgradeData weapon = buyWeapons[i];
            if (equipedWeapon == weapon)
            {
                equipedWeapon = buyWeapons[(i + 1) % buyWeapons.Count];
                return;
            }
        }
    }

    public void loot(int beer, int garbage, int ghost)
    {
        this.beer += beer;
        this.garbage += garbage;
        this.ghost += ghost;
        Save();
        UIManager.Instance.inGameHUD.UpdadeBeerGUI();
        UIManager.Instance.inGameHUD.UpdateGarbageGUI();
        UIManager.Instance.inGameHUD.UpdateGhostGUI();
    }

    public int GetCurrentUpgradeLevel(PlayerUpgradeId upgradeId)
    {
        return playerUpgradeLevel[upgradeId];
    }

    public bool DoubleJumpUnlocked()
    {
        return GetCurrentUpgradeLevel(PlayerUpgradeId.DOUBLE_JUMP) > 0;
    }
    public bool CanDash()
    {
        return GetCurrentUpgradeLevel(PlayerUpgradeId.DASH) > 0;
    }

    public void Buy(PlayerUpgradeId upgradeId)
    {
        int level = GetCurrentUpgradeLevel(upgradeId);
        int cost = PlayerUpgrade.Instance.GetDataFromKind(upgradeId).cost[level];
        if (Ghost >= cost)
        {
            ghost -= cost;
            playerUpgradeLevel[upgradeId] = level + 1;
            UIManager.Instance.inGameHUD.UpdateGhostGUI();
        }
    }


    public bool Buy(WeaponUpgradeData upgradeId)
    {
        if (Beer >= upgradeId.cost)
        {
            beer -= upgradeId.cost;

            equipedWeapon = upgradeId;
            buyWeapons.Add(upgradeId);

            UIManager.Instance.inGameHUD.UpdadeBeerGUI();

            return true;
        }
        return false;
    }
    

}
