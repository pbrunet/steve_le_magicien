using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeUI : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeData playerUpgradedata;

    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMPro.TextMeshProUGUI currentLevelTxt;
    [SerializeField] private TMPro.TextMeshProUGUI nextLevelCostTxt;

    // Start is called before the first frame update
    void Start()
    {
        upgradeImg.sprite = playerUpgradedata.upgradeImg;
        int level = PlayerData.Instance.GetCurrentUpgradeLevel(playerUpgradedata.kind);
        currentLevelTxt.text = level.ToString();
        if (playerUpgradedata.cost.Count > level)
        {
            nextLevelCostTxt.text = "x " + playerUpgradedata.cost[level].ToString();
        } else
        {
            nextLevelCostTxt.text = "LEVEL MAX";
        }
    }
}
