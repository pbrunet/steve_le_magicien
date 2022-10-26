using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingMenu : MonoBehaviour
{
    public event System.Action<BuildingKind> OnBuildingSelected;

    private void OnDisable()
    {
        OnBuildingSelected = null;
    }

    public void OnTowerSelected()
    {
        OnBuildingSelected(BuildingKind.TOWER);
        gameObject.SetActive(false);
    }
    public void OnBarrakSelected()
    {
        OnBuildingSelected(BuildingKind.BARAK);
        gameObject.SetActive(false);
    }
}
