using System.Collections;
using System.Collections.Generic;
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
            float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

            bgmSlider.value = savedBGM;
            sfxSlider.value = savedSFX;

            //슬라이더 값을 강제로 미세하게 조정하여 적용
            StartCoroutine(ForceUpdateSliders());
        }

        // 슬라이더 값 변경 시 볼륨 조절
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private IEnumerator ForceUpdateSliders()
    {
        yield return null; // 한 프레임 대기 (초기화 보장)

        float tempBGM = bgmSlider.value;
        float tempSFX = sfxSlider.value;

        bgmSlider.value = tempBGM + 0.01f;
        sfxSlider.value = tempSFX + 0.01f;

        yield return null; // 한 프레임 더 대기

        bgmSlider.value = tempBGM; // 원래 값으로 복구
        sfxSlider.value = tempSFX;
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
