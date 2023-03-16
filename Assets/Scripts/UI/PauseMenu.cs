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
        ResumeButton.onClick.AddListener(Close);
        QuitButton.onClick.AddListener(HandleQuitClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.Quit();
    }
    private void HandleRestartClicked()
    {
        GameManager.Instance.Restart();
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
