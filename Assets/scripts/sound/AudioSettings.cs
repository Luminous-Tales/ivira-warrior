using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVol, sfxVol;
        mixer.GetFloat("MusicVolume", out musicVol);
        mixer.GetFloat("SFXVolume", out sfxVol);
        musicSlider.value = Mathf.Pow(10, musicVol / 20); // dB -> linear
        sfxSlider.value = Mathf.Pow(10, sfxVol / 20);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
