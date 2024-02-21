using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button respawnButton;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = respawnButton.GetComponent<Button>();

        respawnButton.onClick.AddListener(handleOnClick);

    }

    public void handleOnClick()
    {
        Application.Quit();
    }
}
