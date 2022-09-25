using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float lastSpawnTime = -999.0f;
    [SerializeField] private float spawnDelay;
    [SerializeField] public GameObject[] enemyPrefabs;
    [SerializeField] private float perimWidth;
    [SerializeField] private float perimHeight;

    [SerializeField] private float[] enemyCosts;
    [SerializeField] private float[] enemyStartTimes;
    private float[] enemyBudgets;
    private float managerBudget;
    private float managerBudgetSpeed = 0.0f;
    [SerializeField] private float timeScale = 1.0f;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Spawn(GameObject enemyPrefab) {
        soundManager.playEnemySpawn();

        var t = Random.Range(0.0f, (perimHeight) * 2.0f);
        var maxCorner = new Vector2(perimWidth, perimHeight) * 0.5f;
        var minCorner = -maxCorner;
        var cornerA = new Vector2(maxCorner.x, minCorner.y);
        var cornerB = new Vector2(minCorner.x, maxCorner.y);

        var d = 0.0f;
        var start= Vector2.zero;
        var dir = Vector2.zero;

        if (t < perimHeight) {
            d = t;
            start = maxCorner;
            dir = (cornerA - maxCorner).normalized;
        } else {
            d = t - perimHeight;
            start = cornerB;
            dir = (minCorner - cornerB).normalized;
        }

        var point = start + d * dir;

        Instantiate(enemyPrefab, point, Quaternion.identity);
    }

    [SerializeField] private TMPro.TMP_Text debugText;
    private void FixedUpdate() {
        if (Time.time < FindObjectOfType<NukeManager>().lastNukeTime + 3.0f) {
            return;
        }

        if (enemyBudgets == null || float.IsNaN(enemyBudgets[0])) {
            enemyBudgets = new float[enemyPrefabs.Length];
            for (int i = 0; i < enemyBudgets.Length; i++)
            {
                enemyBudgets[i] = 0f;
            }
        }

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (Time.timeSinceLevelLoad < enemyStartTimes[i]) {
                continue;
            }
            if (i == 0) {
                // debugText.text = enemyBudgets[i] + ", " + enemyCosts[i];
                // debugText.text = enemyPrefabs.Length + ", " + enemyBudgets[i];
            }
            if (enemyBudgets[i] > enemyCosts[i]) {
                enemyBudgets[i] -= enemyCosts[i];
                Spawn(enemyPrefabs[i]);
            } else {
                enemyBudgets[i] += managerBudgetSpeed * Time.deltaTime;
            }
            
        }
            
        managerBudgetSpeed = Mathf.Sqrt(Time.timeSinceLevelLoad * timeScale);
    }

    private void OnDrawGizmosSelected() {
        var maxCorner = new Vector2(perimWidth, perimHeight) * 0.5f;
        var minCorner = -maxCorner;
        // Debug.DrawLine(maxCorner, minCorner);
        var cornerA = new Vector2(maxCorner.x, minCorner.y);
        var cornerB = new Vector2(minCorner.x, maxCorner.y);

        // vertical lines
        Debug.DrawLine(cornerA, maxCorner);
        Debug.DrawLine(cornerB, minCorner);

        // horizontal lines
        Debug.DrawLine(cornerB, maxCorner);
        Debug.DrawLine(cornerA, minCorner);
    }
}
