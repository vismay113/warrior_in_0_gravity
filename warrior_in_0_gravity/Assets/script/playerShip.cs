using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    // configuration variables

    [Header("Player")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float shipPaddingX = 0.5f;
    [SerializeField] float shipPaddingY = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float firingPeriod = 0.1f;

    Coroutine firingCoroutine;

    // othe attached game objects
    [SerializeField] GameObject blueBullet;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;
    [SerializeField] AudioClip bulletSFX;
    [SerializeField] [Range(0, 1)] float bulletSFXVolume = 0.25f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    SceneManger sceneOPS;

    // Start is called before the first frame update
    void Start()
    {
        sceneOPS = FindObjectOfType<SceneManger>();
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

            AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, bulletSFXVolume);

            yield return new WaitForSeconds(firingPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer dealDamage = other.gameObject.GetComponent<DamageDealer>();
        if (!dealDamage) { return; }
        ProcessHits(dealDamage);
    }

    private void ProcessHits(DamageDealer dealDamage)
    {
        health -= dealDamage.GetDamage();
        dealDamage.Hit();

        if (health <= 0)
        {
            DiePlayer();
        }
    }

    private void DiePlayer()
    {
        Destroy(gameObject);
        GameObject explosionVFX = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(explosionVFX, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        sceneOPS.LoadEndMenu();
    }
}