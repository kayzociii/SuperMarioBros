using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    public static SpawnpointManager Instance;

    public List<GameObject> spawnpoints = new();
    public List<GameObject> stages;

    public GameObject player;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        foreach (Transform child in transform){
            spawnpoints.Add(child.gameObject);
        }

        foreach (GameObject point in spawnpoints){
            point.SetActive(false);
        }

        foreach (GameObject stage in stages){
            stage.SetActive(false);
        }

        spawnpoints[0].SetActive(true);
        stages[0].SetActive(true);

        player.transform.position = spawnpoints[0].transform.position;
    }

    public void SetSpawnpoint(){
        for (int i = 0; i < spawnpoints.Count; i++){
            if (spawnpoints[i].active && spawnpoints.Count != 1){
                spawnpoints[i].SetActive(false);
                spawnpoints.Remove(spawnpoints[i]);

                stages[i].SetActive(false);
                stages.Remove(stages[i]);

                SetSpawnpoint();
                break;
            }

            stages[i].SetActive(true);
            spawnpoints[i].SetActive(true);
            player.transform.position = spawnpoints[i].transform.position;

            break;
        }
    }
}
