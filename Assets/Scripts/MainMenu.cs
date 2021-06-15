using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

	public Slider[] volumeSliders;
	public Toggle fullscreenToggle;

	void Start()
	{
		bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
		volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
		volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;

		fullscreenToggle.isOn = isFullscreen;
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
		PlayerPrefs.Save();
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
	}

	public void SetMusicVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
	}

}
