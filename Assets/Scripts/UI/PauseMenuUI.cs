using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicLevelText;
    [SerializeField] private TextMeshProUGUI soundsLevelText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator InitializeUI()
    {
        yield return null;

        soundsLevelText.SetText(SoundEffectManager.Instance.soundsVolume.ToString());
        //musicLevelText.SetText(MusicManager.Instance.musicVolume.ToString());
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;

        StartCoroutine(InitializeUI());
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void IncreaseMusicVolume()
    {
        //MusicManager.Instance.IncreaseMusicVolume();
        //musicLevelText.SetText(MusicManager.Instance.musicVolume.ToString());
    }

    public void DecreaseMusicVolume()
    {
        //MusicManager.Instance.DecreaseMusicVolume();
        //musicLevelText.SetText(MusicManager.Instance.musicVolume.ToString());
    }

    public void IncreaseSoundsVolume()
    {
        SoundEffectManager.Instance.IncreaseSoundsVolume();
        soundsLevelText.SetText(SoundEffectManager.Instance.soundsVolume.ToString());
    }

    public void DecreaseSoundsVolume()
    {
        SoundEffectManager.Instance.DecreaseSoundsVolume();
        soundsLevelText.SetText(SoundEffectManager.Instance.soundsVolume.ToString());
    }

    #region 유효성검사
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicLevelText), musicLevelText);
        HelperUtilities.ValidateCheckNullValue(this, nameof(soundsLevelText), soundsLevelText);
    }

#endif
    #endregion Validation
}
