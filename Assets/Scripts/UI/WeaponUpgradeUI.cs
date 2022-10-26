using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WeaponUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject node;
    [SerializeField] private GameObject arrows;

    private 

    // Start is called before the first frame update
    void Start()
    {
        List<List<WeaponUpgradeData>> weaponPerLevel = WeaponUpgradeManager.Instance.GetWeaponPerLevel();

        RectTransform rec = GetComponent<RectTransform>();

        float heightSpaceBetweenItems = rec.rect.height / (weaponPerLevel.Count + 1); ;
        for (int i = 0; i < weaponPerLevel.Count; i++)
        {
            float spaceBetweenItems = rec.rect.width / (weaponPerLevel[i].Count + 1);
            for(int j=0; j< weaponPerLevel[i].Count; j++)
            {
                GameObject cell = Instantiate(node, UIManager.Instance.weaponUpgradeUI.transform);
                cell.GetComponent<RectTransform>().position = new Vector3(spaceBetweenItems * (j+1) - cell.GetComponent<RectTransform>().rect.width / 2,
                                                                          heightSpaceBetweenItems * (i + 1) - cell.GetComponent<RectTransform>().rect.height / 2,
                                                                          0);

                cell.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = weaponPerLevel[i][j].weaponName;
            }
            // TODO: Spawn arrow and rotate
        }

        // TODO: Spawn the data and child
        // TODO: Look into playerData if it is already unlocked

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
