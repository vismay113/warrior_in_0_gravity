using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configuration variables
    [SerializeField] List<waveConfig> waveList;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool waveLoop = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (waveLoop);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveList.Count; waveIndex++)
        {
            var currentWave = waveList[waveIndex];

            yield return StartCoroutine(SpawAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawAllEnemiesInWave (waveConfig waveConfig)
    {
        for (int enemyIndex = 0; enemyIndex < waveConfig.GetNumberOfEnemies(); enemyIndex++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetPathWayPoints()[0].transform.position, Quaternion.identity);

            newEnemy.GetComponent<enemyPath>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
