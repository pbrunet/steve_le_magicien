using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class EventFadeOutCompleted : UnityEvent<bool> { }

public class UIManager : Singleton<UIManager>
{
    public EventFadeOutCompleted OnNewSceneFadeOutCompleted;

    [SerializeField] private TitleMenuUIManager mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] public InGameHUD inGameHUD;
    [SerializeField] public WeaponUpgradeUI weaponUpgradeUI;
    [SerializeField] private WorkBenchUI workBenchUI;
    [SerializeField] private BestiaireUI bestiaireUI;
    [SerializeField] private SelfImproveUI selfImproveUI;

    [System.Serializable]
    public class KeyToSprite
    {
        public Sprite sprite;
        public string keyName;
    }
    [SerializeField] public List<KeyToSprite> touchSprites = new List<KeyToSprite>(); // /Keyboard/r
    // Start is called before the first frame update

    private void Start()
    {
        if(mainMenu.OnFadeOutCompleted == null)
        {
            mainMenu.OnFadeOutCompleted = new EventFadeOutCompleted();
        }
        if(OnNewSceneFadeOutCompleted == null)
        {
            OnNewSceneFadeOutCompleted = new EventFadeOutCompleted();
        }

        mainMenu.OnFadeOutCompleted.AddListener(HandleOnFadeOutCompleted);

        PlayerController.Instance.OnPauseCB += OnPause;
    }

    private void HandleOnFadeOutCompleted(bool isFadeOut)
    {
        OnNewSceneFadeOutCompleted.Invoke(isFadeOut);
    }

    public void OpenWeaponUpgrade()
    {
        weaponUpgradeUI.Open();
    }
    public void OpenWorkBench()
    {
        workBenchUI.gameObject.SetActive(true);
    }
    public void OpenBestiaire()
    {
        bestiaireUI.Open();
    }
    public void OpenSelfImprovement()
    {
        selfImproveUI.Open();
    }
    public void OpenTitleMenu()
    {
        mainMenu.Open();
    }

    private void OnPause()
    {
        pauseMenu.Open();
    }

    public void ToggleSpeed()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 5;
        } else
        {
            Time.timeScale = 1;
        }
    }
}
