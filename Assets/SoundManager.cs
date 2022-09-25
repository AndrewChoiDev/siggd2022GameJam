using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource growAudio;
    [SerializeField] private AudioSource enemySpawnAudio;
    [SerializeField] private AudioSource enemyDeathAudio;

    public void playGrow() {
        growAudio.Play();
    }
    public void playEnemySpawn() {
        enemySpawnAudio.Play();
    }
    public void playEnemyDeath() {
        enemyDeathAudio.Play();
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
