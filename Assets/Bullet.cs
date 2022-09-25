using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float spawnTime = 0.0f;
    private float lifeDuration = 100.0f;
    [SerializeField] private float maxDist;
    [SerializeField] private float speed;
    private Vector2 startPos;

    void Awake() {
        spawnTime = Time.time;
        startPos = (Vector2)transform.position;
    }

    public void Initiate(Vector2 dir) {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = dir * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > spawnTime + lifeDuration
            || Vector2.Distance((Vector2)transform.position, startPos) > maxDist) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        other.gameObject.GetComponent<IAttackable>()?.Attack(1);
    }
}
