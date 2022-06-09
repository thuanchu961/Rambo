using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public Text GrenadeText;
    [SerializeField] public Text ScoreText;
    [SerializeField] public Text LifeText;
    public GameObject MissionCompletedWindow;
    public GameObject MissionFailedWindow;
    public GameObject GameFinishWindow;
    public Transform parentLevel;
    public GameObject[] levels;
    public Text ScoreFinishText;
    public Text HighScoreFinishText;
    public bool isPaused;
 

    private void Awake()
    {
        instance = this;
        levels = Resources.LoadAll<GameObject>("Level");
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        GameObject oldLevel = Instantiate(levels[PlayerPrefs.GetInt("LevelOfMinigame")], Vector3.zero, Quaternion.identity);
        oldLevel.transform.parent = parentLevel;
        AdmobScript.instance.RequestInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        ShowText();
        ShowFinishText();
    }
    public void ShowRewardAds()
    {
        AdmobScript.instance.ShowRewardAds();
        Debug.Log("show reward ads");
    }
    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("LevelOfMinigame") == levels.Length - 1)
        {
            PlayerPrefs.SetInt("LevelOfMinigame", 0);
            PlayerPrefs.DeleteAll();
            Debug.Log("Game Finish");
        }
        else
        {
            PlayerPrefs.SetInt("LevelOfMinigame", PlayerPrefs.GetInt("LevelOfMinigame") + 1);
            
        }
        SceneManager.LoadScene(1);
        LoadLevel();
        MissionCompletedWindow.SetActive(false);

    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        LoadLevel();
        GameFinishWindow.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
        LoadLevel();
        MissionFailedWindow.SetActive(false);
    }
    public void ShowFinishText()
    {
        ScoreFinishText.text = "Score: "+PlayerPrefs.GetInt("Score").ToString();
        HighScoreFinishText.text = "High Score: " +PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void ShowText()
    {
        if (PlayerController.player_instance.totalLife > 0)
        {
            LifeText.text = "LIFE: " + PlayerController.player_instance.totalLife.ToString();
        }
        else if (PlayerController.player_instance.totalLife <= 0)
        {
            LifeText.text = "LIFE: 0";
        }
        GrenadeText.text = "GRENADE: " + PlayerController.player_instance.totalGrenade.ToString();
        ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
    }
    public void MissionComplete()
    {
        isPaused = true;
        MissionCompletedWindow.SetActive(true);
        Debug.Log("level  completed");
    }

    public void MissionFailed()
    {
        isPaused = true;
        MissionFailedWindow.SetActive(true);
        AdmobScript.instance.ShowInterstitalAds();
    }
    
    public void LoadLevel()
    {
        Destroy(parentLevel.GetChild(0).gameObject);
        GameObject oldLevel = Instantiate(levels[PlayerPrefs.GetInt("LevelOfMinigame")], Vector3.zero, Quaternion.identity);
        oldLevel.transform.parent = parentLevel;
    }


    public void Shoot()
    {
        PlayerController.player_instance.Shoot();
    }
    public void ThrowGrenade()
    {
        PlayerController.player_instance.Throw();
    }
    public void Jump()
    {
        PlayerController.player_instance.Jump();
    }
}
