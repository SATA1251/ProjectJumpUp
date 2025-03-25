using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager instance;
    private string lastLoadScene;

    public int currentStageNum = 0;

    public Button optionButton;
    public Button startButton;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        RegisterButtonEvents();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterButtonEvents();
    }

    private void RegisterButtonEvents()
    {
        optionButton = GameObject.FindWithTag("OptionButton")?.GetComponent<Button>();
        startButton = GameObject.FindWithTag("StartButton")?.GetComponent<Button>();

        if(optionButton != null)
        {
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(LoadOptionScene);
        }

        if(startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(LoadStageSelectScene);
        }
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void LoadReadyRoomScene(int stageNum)
    {
        currentStageNum = stageNum;

       SceneManager.LoadScene("ReadyRoomScene");

    }

    public void LoadReadGameScene()
    {
        switch (currentStageNum)
        {
            case 1:
                SceneManager.LoadScene("Stage1");
                //currentStageNum = 0;
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                // currentStageNum = 0;
                break;
            case 3:
                //currentStageNum = 0;
                break;
            case 4:
               // currentStageNum = 0;
                break;
            default:
                break;
        }
    }

    public void LoadOptionScene()
    {
        // 현재 씬 이름을 저장
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        // 옵션 씬으로 이동
        SceneManager.LoadScene("Option");
    }

}
