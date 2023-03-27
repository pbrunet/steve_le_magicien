using UnityEngine;

public class Speech : MonoBehaviour
{
    [SerializeField] private GameObject dialogBubble;
    [SerializeField] private string text;

    [SerializeField] public Vector3 textOffset;
    [SerializeField] public float duration = 5.0f;

    private GameObject bubble;

    public void OnEnable()
    {
        Invoke("MakeDisapear", duration);

        bubble = Instantiate(dialogBubble, UIManager.Instance.inGameHUD.transform);
        bubble.transform.SetParent(UIManager.Instance.inGameHUD.transform, true);
        bubble.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(this.transform.position + textOffset);
        bubble.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = text.Replace("\\n", "\n");
    }

    public void Update()
    {
        bubble.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(gameObject.transform.position + textOffset);
    }

    public void MakeDisapear()
    {
        Destroy(bubble);
        enabled = false;
    }
}