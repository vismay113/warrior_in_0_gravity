using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPath : MonoBehaviour
{
    // configuration variables
    [SerializeField] waveConfig waveConf;
    List<Transform> enemyWayPonits;
    [SerializeField] float moveSpeed = 2f;
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

    private void moveEnemy()
    {
        if (wayPointIndex <= enemyWayPonits.Count - 1)
        {
            var targetPosition = enemyWayPonits[wayPointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
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
