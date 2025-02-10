using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{

    public static Scene instance;
    private string lastLoadScene;

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

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadStageSelectScene()
    {

    }
    public void LoadReadGameScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadOptionScene()
    {
        lastLoadScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Option");
    }

    public void LoadLastScene() // 옵션씬에서 다시 원래 씬으로 전환
    {
        SceneManager.LoadScene(lastLoadScene);
        lastLoadScene = null;
    }
}
