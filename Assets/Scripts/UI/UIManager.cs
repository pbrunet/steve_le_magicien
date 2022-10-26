using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class EventFadeOutCompleted : UnityEvent<bool> { }

public class UIManager : Singleton<UIManager>
{
    public EventFadeOutCompleted OnNewSceneFadeOutCompleted;

    [SerializeField] private TitleMenuManager mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] public InGameHUD inGameHUD;
    [SerializeField] public WeaponUpgradeUI weaponUpgradeUI;
    [SerializeField] private WorkBenchUI workBenchUI;
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
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleOnFadeOutCompleted(bool isFadeOut)
    {
        OnNewSceneFadeOutCompleted.Invoke(isFadeOut);
    }

    private void HandleGameStateChanged(GameManager.GameState current, GameManager.GameState old)
    {
        //pauseMenu.gameObject.SetActive(current == GameManager.GameState.PAUSED);
        //resultS.gameObject.SetActive(current == GameManager.GameState.END_LEVEL);
        inGameHUD.gameObject.SetActive(current == GameManager.GameState.INGAME);
        mainMenu.gameObject.SetActive(current == GameManager.GameState.TITLE_SCREEN);
    }

    public void OpenWeaponUpgrade()
    {
        weaponUpgradeUI.gameObject.SetActive(true);
    }
    public void OpenWorkBench()
    {
        workBenchUI.gameObject.SetActive(true);
    }
    public void OpenSelfImprovement()
    {
        selfImproveUI.gameObject.SetActive(true);
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
