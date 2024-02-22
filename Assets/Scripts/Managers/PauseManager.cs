using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

    public Slider musicVolume;
    public Slider fxVolume;

    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot unPausedSnapshot;

    Canvas canvas;

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        LoadState();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    public void Pause() {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if (!canvas.enabled) { // Resume button is pressed. Only save in pause menu
            SaveState();
            unPausedSnapshot.TransitionTo(0.01f);
        } else {
            pausedSnapshot.TransitionTo(0.01f);
        }
    }

    public void Quit() {
        SaveState();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void LoadState() {
        Debug.Log("Load volume state");
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0f);
        fxVolume.value = PlayerPrefs.GetFloat("fxVolume", 0f);
    }

    void SaveState() {
        Debug.Log("Save volume state");
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("fxVolume", fxVolume.value);
    }
}