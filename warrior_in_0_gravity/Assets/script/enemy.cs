using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // configuration variables
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float bulletMoveSpeed = 2f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;
    [SerializeField] AudioClip bulletSFX;
    [SerializeField] [Range(0, 1)] float bulletSFXVolume = 0.25f;

    Coroutine enemyFiringCoroutine;
    playerShip player;

    // other attached game object variables
    [SerializeField] GameObject redBullet;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            EnemyFire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void EnemyFire()
    {
        GameObject bullet = Instantiate(redBullet, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, bulletSFXVolume);
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
            DieEnemy();
        }
    }

    private void DieEnemy()
    {
        Destroy(gameObject);
        GameObject explosionVFX = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(explosionVFX, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
