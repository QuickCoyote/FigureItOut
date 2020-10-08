using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : Manager
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
        UpdateOptionsUI(4);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            PausePanel.SetActive(true);
            isUIOpen = true;
        }
    }

    public void UpdateOptionsUI(int id)
    {
        switch(id)
        {
            case 0:
                HorizontalSensitivitySliderInputField.text = HorizontalSensitivitySlider.value.ToString();
                PlayerPrefs.SetFloat("HorizontalSensitivity", HorizontalSensitivitySlider.value);
                return;
            case 1:
                VerticalSensitivitySliderInputField.text = VerticalSensitivitySlider.value.ToString();
                PlayerPrefs.SetFloat("VerticalSensitivity", VerticalSensitivitySlider.value);
                return;
            case 2:
                MusicVolumeSliderInputField.text = MusicVolumeSlider.value.ToString();
                PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
                return;
            case 3:
                SFXVolumeSliderInputField.text = SFXVolumeSlider.value.ToString();
                PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
                return;
            case 4:
                HorizontalSensitivitySliderInputField.text = HorizontalSensitivitySlider.value.ToString();
                PlayerPrefs.SetFloat("HorizontalSensitivity", HorizontalSensitivitySlider.value);
                VerticalSensitivitySliderInputField.text = VerticalSensitivitySlider.value.ToString();
                PlayerPrefs.SetFloat("VerticalSensitivity", VerticalSensitivitySlider.value);
                MusicVolumeSliderInputField.text = MusicVolumeSlider.value.ToString();
                PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
                SFXVolumeSliderInputField.text = SFXVolumeSlider.value.ToString();
                PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
                return;
        }
    }

    public void UpdateSlidersFromInput(int id)
    {
        switch(id)
        {
            case 0:
                float hs = 0.0f;
                float.TryParse(HorizontalSensitivitySliderInputField.text, out hs);
                hs = Mathf.Clamp(hs, 0, 20);

                HorizontalSensitivitySlider.value = hs;
                break;
            case 1:
                float vs = 0.0f;
                float.TryParse(VerticalSensitivitySliderInputField.text, out vs);
                vs = Mathf.Clamp(vs, 0, 20);

                VerticalSensitivitySlider.value = vs;
                break;
            case 2:
                float mv = 0.0f;
                float.TryParse(MusicVolumeSliderInputField.text, out mv);
                mv = Mathf.Clamp(mv, 0, 20);

                MusicVolumeSlider.value = mv;
                break;
            case 3:
                float sv = 0.0f;
                float.TryParse(SFXVolumeSliderInputField.text, out sv);
                sv = Mathf.Clamp(sv, 0, 20);

                SFXVolumeSlider.value = sv;
                break;
        }

        UpdateOptionsUI(id);
    }

    public override void Resume()
    {
        PausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isUIOpen = false;
        GlobalManager.Instance.UpdatePlayerSensitivity();
    }
}
