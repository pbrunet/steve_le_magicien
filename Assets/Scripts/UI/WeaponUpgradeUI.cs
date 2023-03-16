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
            if (PlayerData.Instance.BuyWeapons.Contains(action.weapon))
            {
                action.weaponBtn.GetComponent<Image>().color = new UnityEngine.Color(255, 255, 255, 0);

                action.weaponBtn.onClick.AddListener(() => ShowDescr(action));
            }
            else if (!PlayerData.Instance.UnlockedWeapons.Contains(action.weapon))
            {
                action.weaponBtn.GetComponent<Image>().color = new UnityEngine.Color(255, 0, 0, 175);
                action.weaponBtn.onClick.AddListener(() => OnBuy(action));
            }
        }
    }

    void OnBuy(WeaponAction action)
    {
        Debug.Log(action.weapon.name);
    }

    void ShowDescr(WeaponAction action)
    {
        Debug.Log(action.weapon.name);
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
