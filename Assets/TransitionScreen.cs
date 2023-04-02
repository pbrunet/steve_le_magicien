using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransitionScreen : MonoBehaviour
{

    public event System.Action OnFadeOut;
    public TMP_Text LevelNameTxt;
    public float minDuration = 5;

    private float startTime;
    private Animator animator;
    private CanvasGroup cGroup;

    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        cGroup = GetComponent<CanvasGroup>();
    }

    public void transitionDone()
    {
        float t = 0;
        if(Time.time - startTime < minDuration)
        {
            t = minDuration - (Time.time - startTime);
        }
        Invoke("TerminateTransition", t);
    }

    private void TerminateTransition()
    {
        animator.SetTrigger("FadeIn");
    }

    public void OnFadeInDone()
    {
        gameObject.SetActive(false);
    }

    public void transitionTo(string levelName, bool useFadeOut)
    {
        startTime = Time.time;
        LevelNameTxt.text = levelName;
        gameObject.SetActive(true);
        if(useFadeOut)
        {
            animator.SetTrigger("FadeOut");

        } else
        {
            startTime = Time.time - minDuration;
            animator.SetTrigger("NoFadeOut");
        }
    }
    public void OnFadeOutDone()
    {
        if (OnFadeOut != null)
        {
            OnFadeOut();
        }
    }
}
