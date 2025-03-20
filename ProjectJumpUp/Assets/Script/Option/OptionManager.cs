using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // AudioManager 인스턴스가 존재하는지 확인
        if (AudioManager.Instance != null)
        {
            // 저장된 볼륨 값을 불러와 슬라이더에 반영
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }

        // 슬라이더 값 변경 시 볼륨 조절
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetBGMVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(volume);
        }
    }

    public void CloseOptions()
    {
        // 저장된 씬 이름을 불러와서 해당 씬으로 돌아감
        string previousScene = PlayerPrefs.GetString("PreviousScene", "TitleScene");
        SceneManager.LoadScene(previousScene);
    }
}
