using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject settingPanel;
    public GameObject pausePanel;
    public Image pauseImage;
    public Sprite onPauseSprite;
    public Sprite offPauseSprite;

    private bool _isPaused;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OpenOrCloseSettings()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void OnOrOffPause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pauseImage.sprite = onPauseSprite;
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pauseImage.sprite = offPauseSprite;
        }
    }
}
