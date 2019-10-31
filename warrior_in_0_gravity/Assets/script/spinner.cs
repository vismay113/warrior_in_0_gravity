using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
    // configuration variables
    [SerializeField] float speedOfSpin = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speedOfSpin * Time.deltaTime);
    }
}
