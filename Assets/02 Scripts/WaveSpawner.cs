using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public Wave[] waves;
    public Transform[] spawnEnemiesPoints;
    public Animator anim;
    public Text waveName;
    public int numberEnemiesOfMission;

    private Wave currentWave;
    private int currentWaveNumber;
    private int countWaves = 0;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private bool canAnimate = false;
    public int currentEnemiesKilled = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(wait());
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("waited");
    }
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        if (PlayerPrefs.GetInt("LevelOfMinigame") == 2 || PlayerPrefs.GetInt("LevelOfMinigame") == 3
                || PlayerPrefs.GetInt("LevelOfMinigame") == 4 || PlayerPrefs.GetInt("LevelOfMinigame") == 5
                || PlayerPrefs.GetInt("LevelOfMinigame") == 6 || PlayerPrefs.GetInt("LevelOfMinigame") == 7
                || PlayerPrefs.GetInt("LevelOfMinigame") == 8 || PlayerPrefs.GetInt("LevelOfMinigame") == 9)
        {
            MissionSystem.instance.StatusOfMissionText.text = currentEnemiesKilled.ToString() + "/" + numberEnemiesOfMission;
            if (currentEnemiesKilled == numberEnemiesOfMission)
            {
                MissionSystem.instance.MissionCompleted = true;
            }
        }
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length  == 0  )
        {
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    anim.SetTrigger("WaveComplete");
                    canAnimate = false;
                    Invoke("SpawnNextWave", 5);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("LevelOfMinigame") == 0)
                {
                    PlayerPrefs.SetInt("Mission Complete", 1);  // 1: mission completed || 0: mission failed
                }
             
                Debug.Log("Level finish");
            }
            
        }
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform ramdomPoint = spawnEnemiesPoints[Random.Range(0, spawnEnemiesPoints.Length)];
            Instantiate(randomEnemy, ramdomPoint.position, Quaternion.identity);
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numberOfEnemies == 0)
            {
                if(PlayerPrefs.GetInt("LevelOfMinigame")  == 0)
                {
                    countWaves++;
                    MissionSystem.instance.StatusOfMissionText.text = countWaves.ToString() + "/" + waves.Length;
                }
                else if(PlayerPrefs.GetInt("LevelOfMinigame") == 1)
                {
                    Hostage.instance.Cage.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                
                canSpawn = false;
                canAnimate = true;
            }
        }
        
    }
}
