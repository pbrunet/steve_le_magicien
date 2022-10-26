using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnDefender : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int maxNumUnits;
    public float spawnTime;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<maxNumUnits; i++)
        {
            SpawnDef();
        }
    }

    private void OnDefenderKilled(GameObject def)
    {
        Invoke("SpawnDef", spawnTime);
    }

    private void SpawnDef()
    {
        GameObject obj = Instantiate(defenderPrefab, gameObject.transform);
        obj.transform.position += new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
        //obj.GetComponent<defenderBehavior>().spawner = this;
        obj.GetComponent<Damageable>().OnKilled += OnDefenderKilled;

    }
}
