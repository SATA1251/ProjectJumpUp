using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySavedVolume()
    {
        yield return null; // �� ������ ���

        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    private void Start()
    {
        StartCoroutine(ApplySavedVolume());
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if(bgmSource.clip != bgmClip)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;  // ��� ������ �ݺ� ���
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }


    public void SetBGMVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f); // 0 �� ����
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save(); // ���� ���� ��� ����
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

}
