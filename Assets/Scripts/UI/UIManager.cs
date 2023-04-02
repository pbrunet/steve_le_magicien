using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class EventFadeOutCompleted : UnityEvent<bool> { }

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private TitleMenuUIManager mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] public InGameHUD inGameHUD;
    [SerializeField] public WeaponUpgradeUI weaponUpgradeUI;
    [SerializeField] private WorkBenchUI workBenchUI;
    [SerializeField] private BestiaireUI bestiaireUI;
    [SerializeField] private SelfImproveUI selfImproveUI;
    [SerializeField] public TransitionScreen transitionScreen;

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

        PlayerController.Instance.OnPauseCB += OnPause;
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
    public void CloseTitleMenu()
    {
        mainMenu.gameObject.SetActive(false);
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
