using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

    public AudioMixer masterMixer;

    public void SetFXVolumen(float fxVolume) {
        masterMixer.SetFloat("FXVolume", fxVolume);
    }

    public void SetMusciVolumen(float musicVolume) {
        masterMixer.SetFloat("MusicVolume", musicVolume);
    }
}
