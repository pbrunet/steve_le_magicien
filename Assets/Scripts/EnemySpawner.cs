//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
//    [System.Serializable]
//    public class WaveGroup
//    {
//        public float startDelay = 1f;
//        public int numUnits;
//        public GameObject enemy;
//    }

//    [System.Serializable]
//    public struct Wave
//    {
//        public float startDelay;
//        public int goldOnEarlySpawn;
//        public WaveGroup[] group;
//    }

//    [SerializeField] private Wave[] waves;
//    [SerializeField] private Vector3 spawnLocation;
//    [SerializeField] private GameObject timerUI;

//    private float startWaitTime;
//    private int currentWaveId;
//    private bool waveSpawnInProgress = false;

//    public int currentWave { get { return currentWaveId; } }
//    public bool HasWaveInProgress { get { return waveSpawnInProgress; } }

//    public Wave[] Waves { get { return waves; } }

//    // Start is called before the first frame update
//    void Start()
//    {
//        startWaitTime = Time.time;
//        InvokeRepeating("UpdateUI", 0, 0.1f);

//        Invoke("SpawnNextWave", waves[0].startDelay);
//        currentWaveId = 0;
//    }

//    private void OnMouseDown()
//    {
//        if (!waveSpawnInProgress && currentWaveId < waves.Length)
//        {
//            CancelInvoke("SpawnNextWave");
//            SpawnNextWave();
//            LevelManager.Instance.EarnGold(waves[currentWaveId].goldOnEarlySpawn);
//        }
//    }

//    private void UpdateUI()
//    {
//        if(waveSpawnInProgress)
//        {
//            startWaitTime = Time.time;
//        }
//        timerUI.transform.localScale = new Vector3((Time.time - startWaitTime) / waves[currentWaveId].startDelay, 1, 1);
//    }

//    private void SpawnNextWave()
//    {
//        StartCoroutine(SpawnCurrentWave());
//    }
//    IEnumerator SpawnCurrentWave()
//    {
//        waveSpawnInProgress = true;
//        UIManager.Instance.UpdateWaves();

//        Wave wave = waves[currentWaveId];
//        foreach (WaveGroup wg in wave.group)
//        {
//            yield return new WaitForSeconds(wg.startDelay);
//            for (int i = 0; i < wg.numUnits; i++)
//            {
//                GameObject obj = Instantiate(wg.enemy, spawnLocation, Quaternion.identity);
//                obj.SetActive(true);
//                obj.GetComponent<Damageable>().OnKilled += LevelManager.Instance.EnemyKilled;
//            }
//        }

//        waveSpawnInProgress = false;

//        currentWaveId++;
//        if (currentWaveId < waves.Length)
//        {
//            startWaitTime = Time.time;
//            Invoke("SpawnNextWave", waves[currentWaveId].startDelay);
//        } else
//        {
//            CancelInvoke("UpdateUI");
//        }
//    }
//}
