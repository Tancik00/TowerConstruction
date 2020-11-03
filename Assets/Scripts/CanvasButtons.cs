using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject settingPanel;

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
}
