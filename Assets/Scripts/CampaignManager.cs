using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : Singleton<CampaignManager>
{
    [SerializeField] private List<MonsterData> allMonsters;
    public List<MonsterData> AllMonsters { get { return allMonsters; } }

    public CampaignManager()
    {
    }

    public void StartNewGame()
    {
        GameManager.Instance.Restart();
    }
}
