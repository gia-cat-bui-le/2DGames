using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using static Unity.Collections.AllocatorManager;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    AudioManager audioManager;

    public GameObject pauseMenuUI;
    public GameObject soundPanelUI;

    public Image musicButtonImage;
    public Image sfxButtonImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }

        UpdateButtonImages();
    }

    private void UpdateButtonImages()
    {
        musicButtonImage.sprite = musicSource.mute ? musicOffSprite : musicOnSprite;
        sfxButtonImage.sprite = SFXSource.mute ? sfxOffSprite : sfxOnSprite;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        Debug.Log("music: " + volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        Debug.Log("sfx: " + volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetSFXVolume();
    }

    public void ExitSettingScreen()
    {
        audioManager.PlaySFX(audioManager.click);

        soundPanelUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ToggleMusic()
    {
        audioManager.PlaySFX(audioManager.click);
        musicSource.mute = !musicSource.mute;

        // Change the button image  
        if (musicSource.mute)
        {
            Debug.Log("Music Toggle");
            musicButtonImage.sprite = musicOffSprite;
        }
        else
        {
            Debug.Log("Music Toggle");
            musicButtonImage.sprite = musicOnSprite;
        }

        SetMusicVolume();
    }

    public void ToggleSFX()
    {
        audioManager.PlaySFX(audioManager.click);
        SFXSource.mute = !SFXSource.mute;

        // Change the button image  
        if (SFXSource.mute)
        {
            Debug.Log("SFX Toggle");
            sfxButtonImage.sprite = sfxOffSprite;
        }
        else
        {
            Debug.Log("SFX Toggle");
            sfxButtonImage.sprite = sfxOnSprite;
        }

        SetSFXVolume();
    }
}
