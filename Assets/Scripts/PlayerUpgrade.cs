using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : Singleton<PlayerUpgrade>
{
    [SerializeField] private List<PlayerUpgradeData> allUpgrades;

    public List<PlayerUpgradeData> AllUpgrades { get { return allUpgrades; } }
}
