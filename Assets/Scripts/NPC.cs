using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    [SerializeField]
    private float _radius;
    [SerializeField]
    private GameObject _player;

    private void Update()
    {
        float dis = Vector3.Distance(_player.transform.position, transform.position);
        if (dis <= _radius)
        {
            if (Input.GetKeyDown(KeyCode.E) && !ShopManager.Instance.open)
            {
                if (!ShopManager.Instance.open){
                    ShopManager.Instance.OpenPanel();
                    ShopManager.Instance.GetData();
                }                    
            }
        }
    }
}
