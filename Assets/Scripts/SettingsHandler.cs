using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public enum Setting
{
  Reset,
  MusicVolume,
  SoundVolume,
  ControlType
}

public class SettingsHandler : MonoBehaviour
{
  public Setting setting;

  public void Start()
  {
    switch (setting)
    {
      case Setting.MusicVolume:
        {
          Slider slider = GetComponent<Slider>();
          slider.value = SingleState.Instance.settings.MusicVolume;
          slider.onValueChanged.AddListener((value) =>
          {
            SingleState.Instance.settings.MusicVolume = value;
            SingleState.Instance.settings.SaveSettings();
          });
          break;
        }
      case Setting.SoundVolume:
        {
          Slider slider = GetComponent<Slider>();
          slider.value = SingleState.Instance.settings.SoundVolume;
          slider.onValueChanged.AddListener((value) =>
          {
            SingleState.Instance.settings.SoundVolume = value;
            SingleState.Instance.settings.SaveSettings();
          });
          break;
        }
      case Setting.ControlType:
        {
          TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
          dropdown.value = (int)SingleState.Instance.settings.ControlType;
          break;
        }
    }
  }

  public void SetControlType()
  {
    TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
    SingleState.Instance.settings.ControlType = (ControlType)dropdown.value;
    SingleState.Instance.settings.SaveSettings();
  }

  public void Reset()
  {
    SingleState.Instance.settings = new Settings();
    SingleState.Instance.settings.SaveSettings();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
