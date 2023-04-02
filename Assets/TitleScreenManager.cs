using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        UIManager.Instance.OpenTitleMenu();
    }

    private void OnDisable()
    {
        UIManager.Instance.CloseTitleMenu();
    }
}
