using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel = null;

    [Header("Horizontal Sensitivity")]
    [SerializeField] Slider HorizontalSensitivitySlider = null;
    [SerializeField] TMP_InputField HorizontalSensitivitySliderInputField = null;

    [Header("Vertical Sensitivity")]
    [SerializeField] Slider VerticalSensitivitySlider = null;
    [SerializeField] TMP_InputField VerticalSensitivitySliderInputField = null;

    [Header("Music Volume")]
    [SerializeField] Slider MusicVolumeSlider = null;
    [SerializeField] TMP_InputField MusicVolumeSliderInputField = null;

    [Header("SFX Volume")]
    [SerializeField] Slider SFXVolumeSlider = null;
    [SerializeField] TMP_InputField SFXVolumeSliderInputField = null;

    void Start()
    {
        HorizontalSensitivitySlider.value = PlayerPrefs.GetFloat("HorizontalSensitivity");
        VerticalSensitivitySlider.value = PlayerPrefs.GetFloat("VerticalSensitivity");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            PausePanel.SetActive(true);
        }
    }

    public void UpdateOptionsUI()
    {
        HorizontalSensitivitySliderInputField.text = HorizontalSensitivitySlider.value.ToString();
        VerticalSensitivitySliderInputField.text = VerticalSensitivitySlider.value.ToString();
        MusicVolumeSliderInputField.text = MusicVolumeSlider.value.ToString();
        SFXVolumeSliderInputField.text = SFXVolumeSlider.value.ToString();

        PlayerPrefs.SetFloat("HorizontalSensitivity", HorizontalSensitivitySlider.value);
        PlayerPrefs.SetFloat("VerticalSensitivity", VerticalSensitivitySlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
    }

    public void UpdateSlidersFromInput()
    {
        float hs = 0.0f;
        float.TryParse(HorizontalSensitivitySliderInputField.text, out hs);
        hs = Mathf.Clamp(hs, 0, 20);

        float vs = 0.0f;
        float.TryParse(VerticalSensitivitySliderInputField.text, out vs);
        vs = Mathf.Clamp(vs, 0, 20);

        float mv = 0.0f;
        float.TryParse(MusicVolumeSliderInputField.text, out mv);
        mv = Mathf.Clamp(mv, 0, 20);

        float sv = 0.0f;
        float.TryParse(SFXVolumeSliderInputField.text, out sv);
        sv = Mathf.Clamp(sv, 0, 20);

        HorizontalSensitivitySlider.value = hs;
        VerticalSensitivitySlider.value = vs;
        MusicVolumeSlider.value = mv;
        SFXVolumeSlider.value = sv;

        UpdateOptionsUI();
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
