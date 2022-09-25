using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public static List<Transform> soldierList = new List<Transform>();
    public static Vector2 center;
    [SerializeField] private float combineWeight;
    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private TMPro.TMP_Text currentTimeText;
    [SerializeField] private TMPro.TMP_Text bestTimeText;

    [SerializeField] private float perpSpreadRad;
    [SerializeField] private float sizeForSpread;


    private float timeOfLastShoot = 0.0f;

    private static float bestTime = 0.0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        bestTime = Mathf.Max(bestTime, Time.timeSinceLevelLoad);
        currentTimeText.text = Mathf.FloorToInt(Time.timeSinceLevelLoad).ToString();
        bestTimeText.text = Mathf.FloorToInt(bestTime).ToString();


        
        center = Vector2.zero;
        foreach (var trans in SoldierManager.soldierList)
        {
            center += (Vector2)trans.position;
        }
        center /= SoldierManager.soldierList.Count;

        if (true) {
            Debug.Log("hello");
            foreach (var trans in soldierList)
            {
                var body = trans.GetComponent<Rigidbody2D>();
                body.velocity += (center - body.position).normalized * combineWeight * Time.deltaTime;
            }
        }
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && Time.time > timeOfLastShoot + shootDelay) {
            timeOfLastShoot = Time.time;
            audioSource.PlayOneShot(audioSource.clip);

            var maxSpread = Mathf.Lerp(0.0f, perpSpreadRad, sizeForSpread / soldierList.Count);
            foreach (var trans in soldierList)
            {
                var instance = Instantiate(bulletPrefab, trans.position, Quaternion.identity);
                var bulletComp = instance.GetComponent<Bullet>();
                var bulletDir = (mousePos - center).normalized;
                var spread = Random.Range(-maxSpread, maxSpread);
                bulletDir = (bulletDir + Vector2.Perpendicular(bulletDir) * spread).normalized;
                // Vector2.
                bulletComp.Initiate(bulletDir);
            }
        }
    }
}
