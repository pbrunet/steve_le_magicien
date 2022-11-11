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
}