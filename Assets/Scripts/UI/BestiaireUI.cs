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
    [SerializeField] private float duration;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    private float current = 0;
    private bool next = true;
    private bool inProgress = false;

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
        enemyImg.GetComponent<Image>().sprite = CampaignManager.Instance.AllMonsters[currentPage].monsterImg;
        title.text = CampaignManager.Instance.AllMonsters[currentPage].monsterName;
        desc.text = CampaignManager.Instance.AllMonsters[currentPage].monsterDesc;
    }


    public void GoToNextPage()
    {
        if (!inProgress)
        {
            if (currentPage < CampaignManager.Instance.AllMonsters.Count - 1)
            {
                inProgress = true;
                current = 0;
                next = true;
                currentPage++;
                HideData();
            }
        }
    }

    public void GoToBackPage()
    {
        if (!inProgress)
        {
            if (currentPage > 0)
            {
                inProgress = true;
                current = duration - (duration / sprites.Count);
                next = false;
                currentPage--;
                HideData();
            }
        }
    }

    private void Update()
    {
        if (inProgress)
        {
            float step = duration / sprites.Count;
            Debug.Log((int)(current / step));
            Debug.Log(step);
            Debug.Log(current / step);
            bookImg.GetComponent<Image>().sprite = sprites[(int)(current / step)];

            if (next)
            {
                current += Time.deltaTime;
            }
            else
            {
                current -= Time.deltaTime;
            }

            if (current < 0 || current >= duration)
            {
                inProgress = false;
                DisplayData();
            }
        }
    }
}
