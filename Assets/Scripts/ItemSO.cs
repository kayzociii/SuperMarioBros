using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/New Item`")]
public class ItemSO : ScriptableObject {
    public string itemName;
    public string itemDesc;
    public Sprite icon;
    public int cost;
}
