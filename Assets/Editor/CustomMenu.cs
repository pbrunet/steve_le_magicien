using UnityEngine;
using UnityEditor;

public class CustomMenu
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/UnlockFire")]
    private static void UnlockFire()
    {
        PlayerPrefs.SetInt("Fire Stick", 1);
        PlayerPrefs.Save();
    }

    [MenuItem("Tools/BuyFire")]
    private static void BuyFire()
    {
        PlayerPrefs.SetInt("Fire Stick", 2);
        PlayerPrefs.Save();
    }

    [MenuItem("Tools/BuyAllWeapons")]
    private static void BuyAllWeapons()
    {
        PlayerPrefs.SetInt("version", PlayerData.VERSION);
        PlayerPrefs.SetInt("Baguette archer", 3);
        PlayerPrefs.SetInt("Baguette Degats de zone", 3);
        PlayerPrefs.SetInt("Invocation stick", 3);
        PlayerPrefs.SetInt("Baguette Ninja", 3);
        PlayerPrefs.SetInt("Baguette Normal", 3);
        PlayerPrefs.SetInt("Baguette original", 7);
        PlayerPrefs.SetInt("Baguette Viseur", 3);
        PlayerPrefs.Save();
    }

    [MenuItem("Tools/AddGhost")]
    private static void AddGhost()
    {
        PlayerData.Instance.loot(0, 0, 100);
    }

    [MenuItem("Tools/AddBeer")]
    private static void AddBeer()
    {
        PlayerData.Instance.loot(1000, 0, 0);
    }
}