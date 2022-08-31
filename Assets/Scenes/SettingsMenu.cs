using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine.PostFX;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    private CinemachineVolumeSettings volume;
    public VolumeProfile profile;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" +resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        volume = GameObject.Find("CM vcam1").GetComponent<CinemachineVolumeSettings>();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        _isFullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        if(volume.m_Profile.TryGet<LiftGammaGain>(out var liftGammaGain))
        {
            liftGammaGain.gamma.overrideState = true;

            liftGammaGain.gamma.value = new Vector4(0,0,0, _brightnessLevel);

            volume.m_Profile = profile;
        }
        brightnessTextValue.text = brightness.ToString("0.0");
    }
}
