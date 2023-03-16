using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BestiaireUI : MonoBehaviour
{

    [SerializeField] private RuntimeAnimatorController inBestiaryController;
    [SerializeField] private RuntimeAnimatorController openCloseController;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private Image enemyImg;

    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisplayData();
    }

    private void HideData()
    {
        title.text = "";
        desc.text = "";
        enemyImg.gameObject.SetActive(false);
    }
    private void DisplayData()
    {
        enemyImg.gameObject.SetActive(true);
        enemyImg.sprite = CampaignManager.Instance.AllMonsters[currentPage].monsterImg;
        title.text = CampaignManager.Instance.AllMonsters[currentPage].monsterName;
        desc.text = CampaignManager.Instance.AllMonsters[currentPage].monsterDesc;
    }


    public void GoToNextPage()
    {
        if (currentPage < CampaignManager.Instance.AllMonsters.Count - 1)
        {
            GetComponent<Animator>().runtimeAnimatorController = inBestiaryController;
            GetComponent<Animator>().Play("NextPage");
            currentPage++;
        }
    }

    public void Open()
    {
        PlayerController.Instance.OnDisablePlayer();
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        GetComponent<Animator>().runtimeAnimatorController = openCloseController;
        GetComponent<Animator>().Play("Open");
    }

    public void Close()
    {
        GetComponent<Animator>().runtimeAnimatorController = openCloseController;
        GetComponent<Animator>().Play("Close");
    }
    public void OnClose()
    {
        PlayerController.Instance.OnEnablePlayer();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void GoToBackPage()
    {
        if (currentPage > 0)
        {
            GetComponent<Animator>().runtimeAnimatorController = inBestiaryController;
            GetComponent<Animator>().Play("PreviousPage");
            currentPage--;
        }
    }
}
