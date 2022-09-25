using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBaby : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private float speed;


    private void FixedUpdate() {
        var target = SoldierManager.center;
        var findSink = BabySinkManager.closestSink((Vector2)transform.position);
        if (findSink != null && (findSink.Value - (Vector2)transform.position).magnitude < 80.0f) {
            target = findSink.Value;
        }
        var targetDir = (target - (Vector2)transform.position).normalized;

        var body = GetComponent<Rigidbody2D>();
        body.velocity = targetDir * speed;
    }

    private bool consumed = false;


    private void OnCollisionEnter2D(Collision2D other) {
        if (consumed == false) {
            consumed = true;

            var comp = other.gameObject.GetComponent<BabySink>();
            if (comp == null) {
                FindObjectOfType<SoundManager>().playGrow();
                Instantiate(soldierPrefab, transform.position, Quaternion.identity);
            } else {
                comp.Flash();
            }
        }
        Destroy(gameObject);
    }
}
