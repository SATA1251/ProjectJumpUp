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
        // AudioManager �ν��Ͻ��� �����ϴ��� Ȯ��
        if (AudioManager.Instance != null)
        {
            // ����� ���� ���� �ҷ��� �����̴��� �ݿ�
            float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

            bgmSlider.value = savedBGM;
            sfxSlider.value = savedSFX;

            //�����̴� ���� ������ �̼��ϰ� �����Ͽ� ����
            StartCoroutine(ForceUpdateSliders());
        }

        // �����̴� �� ���� �� ���� ����
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private IEnumerator ForceUpdateSliders()
    {
        yield return null; // �� ������ ��� (�ʱ�ȭ ����)

        float tempBGM = bgmSlider.value;
        float tempSFX = sfxSlider.value;

        bgmSlider.value = tempBGM + 0.01f;
        sfxSlider.value = tempSFX + 0.01f;

        yield return null; // �� ������ �� ���

        bgmSlider.value = tempBGM; // ���� ������ ����
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
        // ����� �� �̸��� �ҷ��ͼ� �ش� ������ ���ư�
        string previousScene = PlayerPrefs.GetString("PreviousScene", "TitleScene");
        SceneManager.LoadScene(previousScene);
    }
}
