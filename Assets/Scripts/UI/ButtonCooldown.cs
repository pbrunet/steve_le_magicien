using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    float timer;
    public Image cooldownImage;

    //set Cooldown Time in seconds here
    public float cooldownTime;

    public void resetTimer() {
        timer = 0;
    }

    void Start()
    {
        timer = cooldownTime;
    }

    void Update()
    {
        timer += Time.deltaTime;
        cooldownImage.fillAmount = 1f - Mathf.Min(1f, timer / cooldownTime);
    }
}
