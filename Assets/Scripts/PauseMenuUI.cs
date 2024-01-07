using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
