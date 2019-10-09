using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configuration variables
    [SerializeField] List<waveConfig> waveList;
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveList[startingWave];

        StartCoroutine(SpawAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawAllEnemiesInWave (waveConfig waveConfig)
    {
        for (int enemyIndex = 0; enemyIndex < waveConfig.GetNumberOfEnemies(); enemyIndex++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetPathWayPoints()[0].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
