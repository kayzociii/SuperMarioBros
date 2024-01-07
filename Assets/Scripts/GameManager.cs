using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private Transform _spawnPoint;
    

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;


        LoadLevel(1, 1);
        
    }

    public void GameOver()
    {
        GameOverUI.Instance.OpenPanel();
        Invoke("NewGame", 5f);
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
        Invoke("UpdateStats", 1f);
    }

    int count = 0;
    public void NextLevel()
    {
        count++;
        if (count == 3){
            StageInfoUI.Instance.OpenPanel();

            return;
        }
        SpawnpointManager.Instance.SetSpawnpoint();
        cam = GameObject.Find("Main Camera");
        //LoadLevel(world, stage + 1);
        //player.transform = _spawnPoint;
        switch(count){
            case 1: 
                cam.transform.position = new Vector3(15.25f + 243f, cam.transform.position.y, cam.transform.position.z);
                break;
            case 2:
                cam.transform.position = new Vector3(565.25f, cam.transform.position.y, cam.transform.position.z);
                break;

        }
        
    }
    
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        count = 0;
        lives--;

        if (lives > 0) {
            coins = 0;
            LoadLevel(world, stage);         
        } else {
            GameOver();
        }
        UpdateStats();
    }

#region Func
    public void AddCoin()
    {
        coins++;
        UpdateStats();
    }

    public void BloodMoney()
    {
        lives = lives - 2;
        coins = coins + 10;
        UpdateStats();
    }

    public void Chosen()
    {
        if(coins >= 10)
        {
            lives++;
        }
        if(coins >= 20)
        {
            coins = coins + 5;
        }
        UpdateStats();
    }

    public void Random()
    {
        Random rand = new Random();
        var Numrd = rand.Next(0,3);
        if(Numrd < 1)
        {
            coins = coins * 2;

        }
        else
        {
            coins = coins >> 1;
        }
        UpdateStats();
    }

    public void DeleteCoin(ItemSO item)
    {
        coins -= item.cost;
        UpdateStats();
    }

    public void ZeroCoin(ItemSO item)
    {
        coins = 0;
        UpdateStats();
    }
    public void AddLife()
    {
        lives++;
        UpdateStats();
    }
#endregion
    public void UpdateStats()
    {
        ShopManager.Instance.cointext.text = coins.ToString();
        ShopManager.Instance.lifetext.text = lives.ToString();
    }
}
