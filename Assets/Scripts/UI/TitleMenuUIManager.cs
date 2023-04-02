using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuUIManager : MonoBehaviour
{

    [SerializeField] private Animation mainMenuAnimation;

    private void Start()
    {
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void OnNewGameClicked()
    {
        CampaignManager.Instance.StartNewGame();
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.Quit();
    }

}
