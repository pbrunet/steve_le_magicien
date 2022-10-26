using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : Singleton<CampaignManager>
{ 

    public CampaignManager()
    {
    }

    public void StartNewGame()
    {
        GameManager.Instance.Restart();
    }
}
