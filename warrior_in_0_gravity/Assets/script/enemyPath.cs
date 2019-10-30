using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPath : MonoBehaviour
{
    // configuration variables
    waveConfig waveConf;
    List<Transform> enemyWayPonits;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyWayPonits = waveConf.GetPathWayPoints();
        transform.position = enemyWayPonits[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    public void SetWaveConfig (waveConfig waveC)
    {
        this.waveConf = waveC;
    }

    private void moveEnemy()
    {
        if (wayPointIndex <= enemyWayPonits.Count - 1)
        {
            var targetPosition = enemyWayPonits[wayPointIndex].transform.position;
            var movementThisFrame = waveConf.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
