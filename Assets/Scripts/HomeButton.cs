using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void Home(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
