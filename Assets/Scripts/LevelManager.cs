//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class LevelManager : Singleton<LevelManager>
//{
//    [SerializeField] private EnemySpawner[] spawners;
//    [SerializeField] private float meteorCooldown = 10;

//    private int enemyLeft = 0;
//    public int EnemyLeft { get { return enemyLeft; } }
//    private int remainingEnemyBeforeEndGame;

//    private int gold = 100;
//    public int Gold { get { return gold; } }

//    private int maxWaves;

//    public int MaxWaves { get { return maxWaves; } }
//    public int CurrentWave
//    {
//        get
//        {
//            int currentWave = 0;
//            foreach (EnemySpawner spawner in spawners)
//            {
//                currentWave += spawner.currentWave;
//                if(spawner.HasWaveInProgress)
//                {
//                    currentWave++;
//                }
//            }
//            return currentWave;
//        }
//    }

//    private GameObject SelectedObject;
//    private System.Action OnUnselect;

//    public event System.Action OnEnableMeteor;
//    public event System.Action OnMeteorShooted;

//    public float MeteorCooldown {  get { return meteorCooldown;  } }

//    #region meteor
//    public void ActivateMeteorPower()
//    {
//        GetComponent<ShootMeteorite>().enabled = true;
//    }

//    private void OnShootMeteor()
//    {
//        Invoke("EnableMeteor", meteorCooldown);
//        if(OnMeteorShooted != null)
//        {
//            OnMeteorShooted();
//        }
//    }

//    private void EnableMeteor()
//    {
//        if (OnEnableMeteor != null)
//        {
//            OnEnableMeteor();
//        }
//    }
//    #endregion

//    public void SelectObject(GameObject obj, System.Action NextOnUnselect)
//    {
//        OnUnselect?.Invoke();
//        SelectedObject = obj;
//        OnUnselect = NextOnUnselect;
//    }

//    public bool Pay(int price)
//    {
//        bool canPay = false;
//        if (gold >= price)
//        {
//            gold -= price;
//            canPay = true;
//        }
//        UIManager.Instance.UpdateGold();
//        return canPay;
//    }

//    public void EarnGold(int price)
//    {
//        gold += price;
//        UIManager.Instance.UpdateGold();
//    }

//    void DiscardEnemy()
//    {
//        if (--remainingEnemyBeforeEndGame == 0)
//        {
//            GameManager.Instance.EndLevel();
//        }
//    }

//    public void EnemyEscaped(GameObject enemy)
//    {
//        enemyLeft += 1;
//        UIManager.Instance.UpdadeEnemyEscapedGUI();
//        DiscardEnemy();
//    }

//    public void EnemyKilled(GameObject enemy)
//    {
//        Reward reward;
//        if (enemy.TryGetComponent<Reward>(out reward))
//        {
//            gold += reward.Gold;
//            UIManager.Instance.UpdateGold();
//        }
//        DiscardEnemy();
//    }

//    private void Start()
//    {
//        foreach (EnemySpawner spawner in spawners)
//        {
//            foreach (EnemySpawner.Wave wave in spawner.Waves)
//            {
//                maxWaves++;
//                foreach (EnemySpawner.WaveGroup wg in wave.group)
//                {
//                    remainingEnemyBeforeEndGame += wg.numUnits;
//                }
//            }
//        }

//        // Initialize meteor callbacks
//        GetComponent<ShootMeteorite>().OnShoot += OnShootMeteor;
//        GetComponent<ShootMeteorite>().OnDisarm += EnableMeteor;

//        // Defer this update until everything is spawned
//        UIManager.Instance.UpdateWaves();
//    }

//}
