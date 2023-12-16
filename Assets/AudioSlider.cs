using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameter;
    public void ChangeVolume(float value)
    {
        mixer.SetFloat(parameter, MathF.Log10(value) * 20);
    }
}
