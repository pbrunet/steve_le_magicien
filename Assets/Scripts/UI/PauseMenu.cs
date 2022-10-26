using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
    }

    private void HandleResumeClicked()
    {
        //GameManager.Instance.TogglePause();
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.Quit();
    }
    private void HandleRestartClicked()
    {
        GameManager.Instance.Restart();
    }
}
