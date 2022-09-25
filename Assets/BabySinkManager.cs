using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySinkManager : MonoBehaviour
{
    public static List<Transform> sinks = new List<Transform>();

    private void Awake() {
        sinks = new List<Transform>();
    }

    public static Vector2? closestSink(Vector2 point) {
        if (sinks.Count == 0) {
            return null;
        }
        var minDistPos = (Vector2)sinks[0].position;
        for (int i = 0; i < sinks.Count; i++)
        {
            var sinkPos = (Vector2)sinks[i].position;
            if ((sinkPos - point).sqrMagnitude < (minDistPos - point).sqrMagnitude) {
                minDistPos = sinkPos;
            }
        }

        return minDistPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
