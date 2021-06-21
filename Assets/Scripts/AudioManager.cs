using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public enum AudioChannel { Master, Sfx, Music };

	public float masterVolumePercent { get; private set; }
	public float sfxVolumePercent { get; private set; }
	public float musicVolumePercent { get; private set; }

	AudioSource[] musicSources;
	int activeMusicSourceIndex;
	int nextActiveMusicSourceIndex1;
	int nextActiveMusicSourceIndex2;

	public static AudioManager instance;

	Transform audioListener;
	public Transform playerT;

	SoundLibrary library;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{

			instance = this;
			DontDestroyOnLoad(gameObject);

			library = GetComponent<SoundLibrary>();

			musicSources = new AudioSource[3];
			for (int i = 0; i < 3; i++)
			{
				GameObject newMusicSource = new GameObject("Music source " + (i + 1));
				musicSources[i] = newMusicSource.AddComponent<AudioSource>();
				newMusicSource.transform.parent = transform;
			}

			audioListener = FindObjectOfType<AudioListener>().transform;
			

			masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
			sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
			musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
		}
	}

	void Update()
	{
        if (FindObjectOfType<PlayerMovement>() != null)
        {
            playerT = FindObjectOfType<PlayerMovement>().transform;
        }
        if (playerT != null)
		{
			audioListener.position = playerT.position;
		}
	}

	public void SetVolume(float volumePercent, AudioChannel channel)
	{
		switch (channel)
		{
			case AudioChannel.Master:
				masterVolumePercent = volumePercent;
				break;
			case AudioChannel.Sfx:
				sfxVolumePercent = volumePercent;
				break;
			case AudioChannel.Music:
				musicVolumePercent = volumePercent;
				break;
		}

		musicSources[0].volume = musicVolumePercent * masterVolumePercent;
		musicSources[1].volume = musicVolumePercent * masterVolumePercent;
		musicSources[2].volume = musicVolumePercent * masterVolumePercent;

		PlayerPrefs.SetFloat("master vol", masterVolumePercent);
		PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
		PlayerPrefs.SetFloat("music vol", musicVolumePercent);
		PlayerPrefs.Save();
	}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1)
	{
		activeMusicSourceIndex = SceneManager.GetActiveScene().buildIndex;
		nextActiveMusicSourceIndex1 = (activeMusicSourceIndex + 1) % 3;
		nextActiveMusicSourceIndex2 = (activeMusicSourceIndex + 2) % 3;
		musicSources[activeMusicSourceIndex].clip = clip;
		musicSources[activeMusicSourceIndex].Play();

		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}

	public void PlaySound(AudioClip clip, Vector3 pos)
	{
		if (clip != null)
		{
			AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}

	public void PlaySound(string soundName, Vector3 pos)
	{
		PlaySound(library.GetClipFromName(soundName), pos);
	}

	IEnumerator AnimateMusicCrossfade(float duration)
	{
		float percent = 0;

		while (percent < 1)
		{
			percent += Time.deltaTime * 1 / duration;
			musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
			musicSources[nextActiveMusicSourceIndex1].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
			musicSources[nextActiveMusicSourceIndex2].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
			yield return null;
		}
	}
}
