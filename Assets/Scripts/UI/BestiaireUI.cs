using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaireUI : MonoBehaviour
{

    [SerializeField] private GameObject bookImg;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private GameObject enemyImg;

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
        enemyImg.SetActive(false);
    }
    private void DisplayData()
    {
        enemyImg.SetActive(true);
        title.text = CampaignManager.Instance.AllMonsters[currentPage].monsterName;
        desc.text = CampaignManager.Instance.AllMonsters[currentPage].monsterDesc;
    }


    public void GoToNextPage()
    {
        if (currentPage < CampaignManager.Instance.AllMonsters.Count - 1)
        {
            GetComponent<Animator>().Play("NextPage");
            currentPage++;
        }
    }

    public void GoToBackPage()
    {
        if (currentPage > 0)
        {
            GetComponent<Animator>().Play("PreviousPage");
            currentPage--;
        }
    }
}
