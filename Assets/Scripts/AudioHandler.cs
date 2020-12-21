using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    [Range(0,1)] public float DefaultVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", DefaultVolume);
        MasterVolumeSlider.value = volume;
        setMasterVolume(volume);
    }

    private void setMasterVolume(float volume)
    {
        GetComponent<AudioSource>().volume = volume;
    }

    public void SetMasterVolumeSlider(float volume)
    {
        setMasterVolume(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        //FindObjectOfType<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject ;
    }
}
