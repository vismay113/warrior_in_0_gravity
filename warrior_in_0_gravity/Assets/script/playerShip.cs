using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    // configuration variables
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float shipPaddingX = 0.5f;
    [SerializeField] float shipPaddingY = 0.5f;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float firingPeriod = 0.1f;

    Coroutine firingCoroutine;

    // othe attached game objects
    [SerializeField] GameObject blueBullet;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        MoveBoundary();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        FireBullet();
    }

    private void MoveBoundary()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + shipPaddingX;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - shipPaddingX;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + shipPaddingY;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - shipPaddingY;
    }

    private void MoveShip()
    {
        var deltaX = Input.GetAxis("Horizontal") * (Time.deltaTime * moveSpeed);
        var deltaY = Input.GetAxis("Vertical") * (Time.deltaTime * moveSpeed);

        var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void FireBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine =  StartCoroutine(FireRepeatedly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireRepeatedly()
    {
        while (true)
        {
            GameObject bullet = Instantiate(blueBullet, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(firingPeriod);
        }
    }
}