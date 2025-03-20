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
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }

        // �����̴� �� ���� �� ���� ����
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
        // ����� �� �̸��� �ҷ��ͼ� �ش� ������ ���ư�
        string previousScene = PlayerPrefs.GetString("PreviousScene", "TitleScene");
        SceneManager.LoadScene(previousScene);
    }
}
