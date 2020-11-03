using UnityEngine;

public class GameModeController : MonoBehaviour
{
    public string gameMode;

    public void SetCubeSpeed()
    {
        switch (gameMode)
        {
            case "easy": PlayerPrefs.SetFloat("cubeSpeed", 0.5f);
                break;
            case "medium": PlayerPrefs.SetFloat("cubeSpeed", 0.3f);
                break;
            case "hard": PlayerPrefs.SetFloat("cubeSpeed", 0.1f);
                break;
        }
    }
}
