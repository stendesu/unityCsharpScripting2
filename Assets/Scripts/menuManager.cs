using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    public GameObject m_controls_Settings_Panel;
    bool isCSPanelOpen = false;
    public Button playButton;
    public Button controlsButton;
    public Button quitButton;
    public Button backButton;
    public GameObject canvas;

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level0", LoadSceneMode.Single);
        SceneManager.LoadScene("UI_PauseMenu", LoadSceneMode.Additive);
        canvas.SetActive(false);
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
        playButton.onClick.AddListener(LoadLevel1);
        controlsButton.onClick.AddListener(OpenAndCloseSettingsPanel);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(OpenAndCloseSettingsPanel);
    }

}
