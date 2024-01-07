using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour {
    Action<ItemSO> OnClickInfoCallBack;

    [SerializeField]
    private TMP_Text _itemName;
    [SerializeField]
    private TMP_Text _itemDesc;
    [SerializeField]
    private Image _itemIcon;
    [SerializeField]
    private TMP_Text _itemCost;

    [SerializeField]
    private ItemSO _data;

    public void InitData(ItemSO itemData, Action<ItemSO> callBack) {
        _itemName.text = itemData.itemName;
        _itemDesc.text = itemData.itemDesc;
        _itemIcon.sprite = itemData.icon;
        _itemCost.text = "Cost: " + itemData.cost.ToString();

        if (GameManager.Instance.coins < itemData.cost){
            _itemCost.color = Color.red;
            gameObject.GetComponent<Button>().enabled = false;
        }

        _data = itemData;
        OnClickInfoCallBack = callBack;
    }

    public void OnClickInfo(){
        OnClickInfoCallBack?.Invoke(_data);
    }
}
