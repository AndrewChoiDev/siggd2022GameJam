using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] private int health;

    private float lastHitTime = -99999f;

    [SerializeField] private Color hitColor;
    [SerializeField] private float hitFlashDuration;
    [SerializeField] private GameObject soldierBabyPrefab;
    private Color baseColor;

    [SerializeField] private GameObject bulletPrefab;



    private float lastShootTime = -99999f;
    [SerializeField] private float shootDelay;
    [SerializeField] private float startDelay;
    private float startTime;

    [SerializeField] private float approachSpeed;


    private SoundManager  soundManager;

    private void Awake() {
        baseColor = GetComponent<SpriteRenderer>().color;
        soundManager = FindObjectOfType<SoundManager>();

        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        var sprRender = GetComponent<SpriteRenderer>();

        if (Time.time > lastHitTime + hitFlashDuration) {
            sprRender.color = baseColor;
        } else {
            sprRender.color = hitColor;
        }
    }

    private void FixedUpdate() {
        if (Time.time > lastShootTime + shootDelay 
            && Time.time > startTime + startDelay) {
            lastShootTime = Time.time;
            var dir = (SoldierManager.center - (Vector2)transform.position).normalized;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>().Initiate(dir);
        }

        var body = GetComponent<Rigidbody2D>();
        body.velocity = (SoldierManager.center - (Vector2)transform.position).normalized * approachSpeed;
    }

    public void Attack(int amount) {
        health = Mathf.Max(0, health - amount);
        if (health == 0) {
            Destroy(gameObject);
            soundManager.playEnemyDeath();
            Instantiate(soldierBabyPrefab, transform.position, Quaternion.identity);
        } else {
            // soundManager.playEnemyHurt();
        }

        lastHitTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("player")) {
            Destroy(other.gameObject);
        }
    }
}
