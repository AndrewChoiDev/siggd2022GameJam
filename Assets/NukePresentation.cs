using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePresentation : MonoBehaviour
{
    [SerializeField] private Color nukeColor;
    [SerializeField] private float nukeDuration;
    [SerializeField] private GameObject iconRender;
    public float lastNuke = -999.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sprRender = GetComponent<SpriteRenderer>();
        sprRender.color = Color.Lerp(nukeColor, Color.black, (Time.time - lastNuke) / nukeDuration);
    }

    public void Nuke() {
        lastNuke = Time.time;
        GetComponent<AudioSource>().Play();
        Destroy(iconRender);
    }
}
