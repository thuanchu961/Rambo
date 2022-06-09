using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float yOffset;
    [SerializeField] public float xOffset;
     private Transform target;


    float MinX = 0f;
    float MaxX;


    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelOfMinigame") == 0 || PlayerPrefs.GetInt("LevelOfMinigame") == 2 || PlayerPrefs.GetInt("LevelOfMinigame") == 4 || PlayerPrefs.GetInt("LevelOfMinigame") == 6 || PlayerPrefs.GetInt("LevelOfMinigame") == 8)
        {
            MaxX = 19f;
        }
        else if (PlayerPrefs.GetInt("LevelOfMinigame") == 1 || PlayerPrefs.GetInt("LevelOfMinigame") == 3 || PlayerPrefs.GetInt("LevelOfMinigame") == 5 || PlayerPrefs.GetInt("LevelOfMinigame") == 7 || PlayerPrefs.GetInt("LevelOfMinigame") == 9)
        {
            MaxX = 33.4f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = PlayerController.player_instance.transform;
        CamMove();
    }

    void CamMove()
    {
       
        
        Vector3 newPos = new Vector3(Mathf.Clamp(target.position.x,MinX,MaxX), 0f, -10f);
        Vector3 smoothPos = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
