using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    public GameObject objectToDestroy;
    public GameObject victoryPanel;
    public PauseMenu pauseMenu;

    public void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if (objectToDestroy == null)
        {
        
            PauseMenu.isPaused = true;

            Time.timeScale = 0f;
            victoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
