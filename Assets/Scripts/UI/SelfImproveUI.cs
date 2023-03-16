using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfImproveUI : MonoBehaviour
{
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
