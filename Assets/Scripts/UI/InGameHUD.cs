using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private TextMeshProUGUI beer;
    [SerializeField] private TextMeshProUGUI garbage;

    public void UpdadeLifeGUI(Damageable player)
    {
        life.SetText("Life : " + player.Life);
    }

    public void UpdadeBeerGUI()
    {
        beer.SetText("Beer : " + PlayerData.Instance.Beer);
    }

    public void UpdateGarbageGUI()
    {
        garbage.SetText("Garbage : " + PlayerData.Instance.Garbage);
    }

    private void Start()
    {
        UpdadeBeerGUI();
        UpdateGarbageGUI();
    }
}
