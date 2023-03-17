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
        PlayerPrefs.SetInt("Baguette archer", 2);
        PlayerPrefs.SetInt("Baguette Dégats de zone", 2);
        PlayerPrefs.SetInt("Invocation stick", 2);
        PlayerPrefs.SetInt("Baguette Ninja", 2);
        PlayerPrefs.SetInt("Baguette Normal", 2);
        PlayerPrefs.SetInt("Baguette original", 2);
        PlayerPrefs.SetInt("Baguette Viseur", 2);
        PlayerPrefs.Save();
    }

    [MenuItem("Tools/AddGhost")]
    private static void AddGhost()
    {
        PlayerData.Instance.loot(0, 0, 100);
    }
}