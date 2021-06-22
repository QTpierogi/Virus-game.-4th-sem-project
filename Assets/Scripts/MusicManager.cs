using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public AudioClip menuTheme;
	public AudioClip mainTheme;
	public AudioClip deathTheme;
	public AudioClip bossTheme;

	string sceneName;

	void Start()
	{
		OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
	}


	void OnLevelWasLoaded(int sceneIndex)
	{
		string newSceneName = SceneManager.GetActiveScene().name;
		if (newSceneName != sceneName)
		{
			sceneName = newSceneName;
			Invoke("PlayMusic", .2f);
		}
	}

	void PlayMusic()
	{
		AudioClip clipToPlay = null;

		if (sceneName == "MenuScene")
		{
			clipToPlay = menuTheme;
		}
		else if (sceneName == "DemoLevel")
		{
			clipToPlay = mainTheme;
		}
		else if (sceneName == "BossRoom")
		{
			clipToPlay = bossTheme;
		}
		else if (sceneName == "GameOver")
		{
			clipToPlay = deathTheme;
		}

		if (clipToPlay != null)
		{
			AudioManager.instance.PlayMusic(clipToPlay, 0);
			Invoke("PlayMusic", clipToPlay.length);
		}

	}
}
