using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : Singleton<PlayerUpgrade>
{
    [SerializeField] private List<PlayerUpgradeData> allUpgrades;

    public List<PlayerUpgradeData> AllUpgrades { get { return allUpgrades; } }

    public PlayerUpgradeData GetDataFromKind(PlayerUpgradeId upgradeId)
    {
        return allUpgrades.Find(pud => pud.kind == upgradeId);
    }

}
