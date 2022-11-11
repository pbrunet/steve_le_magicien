using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.UIElements;
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
            if (PlayerData.Instance.BuyWeapons.Contains(action.weapon.weaponName))
            {
                action.weaponBtn.GetComponent<Image>().color = new UnityEngine.Color(255, 255, 255, 0);

                action.weaponBtn.onClick.AddListener(() => ShowDescr(action));
            }
            else if (!PlayerData.Instance.UnlockedWeapons.Contains(action.weapon.weaponName))
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

    //RectTransform rec = GetComponent<RectTransform>();

    //float heightSpaceBetweenItems = rec.rect.height / (weaponPerLevel.Count + 1); ;
    //for (int i = 0; i < weaponPerLevel.Count; i++)
    //{
    //    float spaceBetweenItems = rec.rect.width / (weaponPerLevel[i].Count + 1);
    //    for(int j=0; j< weaponPerLevel[i].Count; j++)
    //    {
    //        GameObject cell = Instantiate(node, UIManager.Instance.weaponUpgradeUI.transform);
    //        cell.GetComponent<RectTransform>().position = new Vector3(spaceBetweenItems * (j+1) - cell.GetComponent<RectTransform>().rect.width / 2,
    //                                                                  heightSpaceBetweenItems * (i + 1) - cell.GetComponent<RectTransform>().rect.height / 2,
    //                                                                  0);

    //        cell.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(). = weaponPerLevel[i][j].weaponName;
    //    }
    //    // TODO: Spawn arrow and rotate
    //}

    //// TODO: Spawn the data and child
    //// TODO: Look into playerData if it is already unlocked

    //}

    // Update is called once per frame
    void Update()
    {

    }
}
