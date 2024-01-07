using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public GameObject gameOverPanel;

    private void Awake(){
        Instance = this;
    }

    public void OpenPanel(){
        gameOverPanel.SetActive(true);
    }
}
