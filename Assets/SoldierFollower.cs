using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFollower : MonoBehaviour
{
    [SerializeField] private float radMultiplier;

    // Update is called once per frame
    void Update()
    {
        var center = SoldierManager.center;
        transform.position = center;
        transform.position += 10.0f * Vector3.back;

        var maxRad = 0.0f;

        foreach (var trans in SoldierManager.soldierList)
        {
            var rad = ((Vector2)trans.position - center).magnitude;
            maxRad = Mathf.Max(rad, maxRad);
        }

        GetComponent<Camera>().orthographicSize = maxRad * radMultiplier + 4.0f;
    }
}
