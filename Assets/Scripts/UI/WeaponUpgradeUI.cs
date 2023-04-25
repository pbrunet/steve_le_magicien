using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeUI : MonoBehaviour
{
    [System.Serializable]
    struct WeaponAction
    {
        [SerializeField] public Button weaponBtn;
        [SerializeField] public WeaponUpgradeData weapon;
    }

    [SerializeField] List<WeaponAction> allWeaponActions;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var action in allWeaponActions)
        {
            if (!PlayerData.Instance.BuyWeapons.Contains(action.weapon))
            {
                action.weaponBtn.GetComponent<Image>().color = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1);

                action.weaponBtn.onClick.AddListener(() => OnBuy(action));
            }
        }
    }

    void OnBuy(WeaponAction action)
    {
        if (PlayerData.Instance.Buy(action.weapon))
        {
            action.weaponBtn.GetComponent<Image>().color = new UnityEngine.Color(1, 1, 1, 1);
            action.weaponBtn.onClick.RemoveAllListeners();
        }
    }

    public void Open()
    {
        PlayerController.Instance.OnDisablePlayer();
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        GetComponent<Animator>().Play("Open");
    }
    public void Close()
    {
        GetComponent<Animator>().Play("Close");
    }
    public void OnClose()
    {
        PlayerController.Instance.OnEnablePlayer();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
