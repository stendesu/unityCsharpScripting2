using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
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
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
