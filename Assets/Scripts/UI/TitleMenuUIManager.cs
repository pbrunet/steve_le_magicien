using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuUIManager : MonoBehaviour
{
    public EventFadeOutCompleted OnFadeOutCompleted;

    [SerializeField] private Animation mainMenuAnimation;
    [SerializeField] private AnimationClip fadeOut;
    [SerializeField] private AnimationClip fadeIn;

    private void Start()
    {
        //FadeIn();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void FadeInCompleted()
    {
        OnFadeOutCompleted.Invoke(false);
    }

    public void FadeOutCompleted()
    {
        Debug.Log("fadeOutDone");
        gameObject.SetActive(false);
        OnFadeOutCompleted.Invoke(true);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        mainMenuAnimation.Stop();
        mainMenuAnimation.clip = fadeIn;
        mainMenuAnimation.Play();
    }

    public void FadeOut()
    {
        Debug.Log("FadeOutStart");
        mainMenuAnimation.Stop();
        mainMenuAnimation.clip = fadeOut;
        mainMenuAnimation.Play();
    }

    public void OnNewGameClicked()
    {
        //FadeOut();
        CampaignManager.Instance.StartNewGame();
        gameObject.SetActive(false);
    }

    public void OnQuitClicked()
    {
        //FadeOut();

        GameManager.Instance.Quit();
    }

}
