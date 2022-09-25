using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySink : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private void Awake() {
        BabySinkManager.sinks.Add(this.transform);
    }

    private void OnDestroy() {
        BabySinkManager.sinks.Remove(this.transform);
    }

    public void Flash()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>()
            .Initiate((SoldierManager.center - (Vector2)transform.position).normalized);
    }
}
