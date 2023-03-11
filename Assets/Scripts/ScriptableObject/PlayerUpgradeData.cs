using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerUpgradeId
{
    BANK,
    COLLECT,
    DASH,
    DOUBLE_JUMP,
    FLY, HEALTH, HEALTH2, ON_FIRE
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerUpgradeData", order = 1)]
public class PlayerUpgradeData : ScriptableObject
{
    public Sprite upgradeImg;
    public string upgradeName;
    public List<int> cost;
    public PlayerUpgradeId kind;
}