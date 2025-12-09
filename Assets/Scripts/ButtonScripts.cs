using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{

    public void HandlePlayClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void HandleRestartClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void HandleMenuClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
    }
}
