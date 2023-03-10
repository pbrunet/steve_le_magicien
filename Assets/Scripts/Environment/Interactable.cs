using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private string actionName;

    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] public Vector3 textOffset;

    private GameObject tooltip;

    public void OnDestroy()
    {
        Destroy(tooltip);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InputActionMap actionMap = actionAsset.FindActionMap("Player");
            InputAction action = actionMap.FindAction(actionName);
            action.started += DoInteract;
            if (tooltipPrefab != null)
            {
                tooltip = Instantiate(tooltipPrefab, UIManager.Instance.inGameHUD.transform);
                tooltip.transform.SetParent(UIManager.Instance.inGameHUD.transform, true);
                tooltip.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(this.transform.position + textOffset);
                tooltip.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = actionName;
                tooltip.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = UIManager.Instance.touchSprites.Find(kts => kts.keyName == action.controls[0].path)?.sprite;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (tooltip != null && collision.gameObject.tag == "Player")
        {
            InputActionMap actionMap = actionAsset.FindActionMap("Player");
            InputAction action = actionMap.FindAction(actionName);
            action.started -= DoInteract;
            Destroy(tooltip);
        }
    }

    public void Update()
    {
        if (tooltip != null)
        {
            tooltip.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(gameObject.transform.position + textOffset);
        }
    }

    public virtual void DoInteract(InputAction.CallbackContext cb)
    {
        Destroy(tooltip);
        tooltip = null;
    }
}
