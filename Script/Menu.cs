using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GuidePanel;

    // Cac botton o MenuStart Game
    private void Start()
    {
        DontDestroyOnLoad(AudioManager.instance.Click);
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
    }
    public void EnterPlay()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void EnterQuit()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        Application.Quit();
    }

    // Cac Menu trong PanelPause
    public void EnterExit()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Guide()
    {
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Click);
        GuidePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
