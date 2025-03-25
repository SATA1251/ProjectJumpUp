using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    public Button button;
    [SerializeField] private int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if(GameSceneManager.instance != null)
        {
            GameSceneManager.instance.LoadReadyRoomScene(sceneToLoad);           
        }
    }

}
