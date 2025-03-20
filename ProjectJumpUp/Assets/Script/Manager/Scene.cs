using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{

    public static Scene instance;
    private string lastLoadScene;

    int currentStageNum = 0;

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
        if (optionButton != null)
        {
            optionButton.onClick.AddListener(LoadOptionScene);
        }

        if (startButton != null)
        {
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
                SceneManager.LoadScene("Main");
                currentStageNum = 0;
                break;
            case 2:
                currentStageNum = 0;
                break;
            case 3:
                currentStageNum = 0;
                break;
            case 4:
                currentStageNum = 0;
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
