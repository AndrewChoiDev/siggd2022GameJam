using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeManager : MonoBehaviour
{
    private bool hasUsed = false;
    public float lastNukeTime = -999.9f;

    [SerializeField] private GameObject soldierBaby;

    // Update is called once per frame
    void Update()
    {
        if (hasUsed == false && Input.GetKeyDown(KeyCode.Space)) {
            hasUsed = true;

            var enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                Instantiate(soldierBaby, enemy.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(soldierBaby, enemy.transform.position + Vector3.down, Quaternion.identity);
                Destroy(enemy.gameObject);
            }
            var bullets = FindObjectsOfType<Bullet>();
            foreach (var bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
            FindObjectOfType<NukePresentation>().Nuke();
            lastNukeTime = Time.time;
        }
    }
}
