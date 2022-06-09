using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Missions 
{
    public string missionOfLevel;
    public int IDMission;
    public string description;
    public bool isDone;
}

public class MissionSystem : MonoBehaviour
{
    public static MissionSystem instance;
    public Missions[] missions;
    public Text MissionText;
    public Text StatusOfMissionText;
    public bool MissionCompleted;

    private Missions currentMission;
    private int currentMissionNumber;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        ShowMission();

        currentMissionNumber = PlayerPrefs.GetInt("LevelOfMinigame");
        currentMission = missions[currentMissionNumber];

        CheckMission();
        if (currentMission.isDone)
        {
            GameManager.instance.MissionComplete();
        }
    }

    public void ShowMission()
    {
        for (int i = 0; i < GameManager.instance.levels.Length; i++)
        {
            if(PlayerPrefs.GetInt("LevelOfMinigame") == i)
            {
                MissionText.text = missions[i].description.ToString();
            }
        }
    }

    public void CheckMission()
    {
        switch (currentMissionNumber)
        {
            case 0: // mission level 1
                if (PlayerPrefs.GetInt("Mission Complete") == 1)
                {
                    currentMission.isDone = true;
                }
                break;
            case 1: //mission level 2
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 2: // mission level 3
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 3: // mission level 4
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 4: // mission level 5
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 5: // mission level 6
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 6:  // mission level 7
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 7: // mission level 8
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;
            case 8: // mission level 9
                if (MissionCompleted)
                {
                    currentMission.isDone = true;
                    MissionCompleted = false;
                }
                break;

        }
    }


}
