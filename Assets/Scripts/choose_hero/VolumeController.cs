using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        // 获取当前音量值并设置滑动条的初始值
        float currentVolume = 0f;
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void SetVolume(float volume)
    {
        // 根据滑动条的值设置音量参数
        audioMixer.SetFloat("MasterVolume", volume);
    }
}