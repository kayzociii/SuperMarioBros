using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInfoUI : MonoBehaviour {
    public static StageInfoUI Instance;

    public GameObject stageInfoPanel;
    public GameObject player;

    private void Awake(){
        Instance = this;
    }

    public void OpenPanel(){
        stageInfoPanel.SetActive(true);
        player.SetActive(false);

        Invoke("LoadMainMenu", 3f);
    }

    protected void LoadMainMenu(){
        SceneManager.LoadScene("Title");
    }
}
