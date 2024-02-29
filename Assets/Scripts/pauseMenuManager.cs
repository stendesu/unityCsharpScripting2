using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class pauseMenuManager : MonoBehaviour
{
    public GameObject m_controls_Settings_Panel;
    bool isCSPanelOpen = false;
    public Button resumeButton;
    public Button controlsButton;
    public Button quitButton;
    public Button mainMenuButton;
    public Button backButton;
    public GameObject canvas;
    public bool paused = false;

    public void deactivateCanvas()
    {
        canvas.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    public void activateCanvas()
    {
        canvas.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OpenAndCloseSettingsPanel()
    {
        if (isCSPanelOpen == false)
        {
            isCSPanelOpen = true;
            m_controls_Settings_Panel.SetActive(isCSPanelOpen);
        }
        else if (isCSPanelOpen == true)
        {
            isCSPanelOpen = false;
            m_controls_Settings_Panel.SetActive(isCSPanelOpen);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    void Start()
    {
        deactivateCanvas();
        resumeButton.onClick.AddListener(deactivateCanvas);
        controlsButton.onClick.AddListener(OpenAndCloseSettingsPanel);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(OpenAndCloseSettingsPanel);
        mainMenuButton.onClick.AddListener(loadMainMenu);
    }
}
