using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour {
    public static ShopManager Instance;

    [SerializeField]
    private string _path;
    [SerializeField]
    private GameObject _shopItemPrefab;
    [SerializeField]
    private GameObject _shopContainer;
    [SerializeField]
    public TMP_Text cointext;
    [SerializeField]
    public TMP_Text lifetext;
    
    [Space(5)]
    [Header("Error Message")]
    [SerializeField]
    private TMP_Text errorText;
    [Space(5)]

    public List<ItemSO> _items = new();
    public List<GameObject> _shopItemList = new();

    public GameObject panel;

    [HideInInspector]
    public bool open = false;

    public Player player;

    private Rigidbody2D rb;

    private void Awake(){
        Instance = this;
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void Start(){
        ResetList();

        panel.SetActive(false);
    }

    public void ClearItem(){
        ResetList();
        ClearSpawnItemList();
    }

    public void GetData(){
        for (int i = _items.Count - 1; i >= 0; i--){
            _items.Remove(_items[Random.Range(0, _items.Count)]);

            if (_items.Count < 4){
                break;
            }
        }

        _items = _items.OrderBy(i => System.Guid.NewGuid()).ToList();

        SpawnItem();
    }

    public void SpawnItem(){
        foreach(ItemSO item in _items){
            var shopItem = Instantiate(_shopItemPrefab, _shopContainer.transform);
            shopItem.GetComponent<ShopItem>().InitData(item, OnClickInfo);
            _shopItemList.Add(shopItem.gameObject);
        }
    }

    private void ClearSpawnItemList(){
        if (_shopItemList == null)
            return;

        foreach(GameObject shopItem in _shopItemList){
            Destroy(shopItem);
        }

        _shopItemList.Clear();
    }

    private void ResetList(){
        if (_items != null)
            _items.Clear();

        ItemSO[] datas = Resources.LoadAll<ItemSO>(_path);
        foreach(ItemSO data in datas){
            _items.Add(data);
        }
    }

    private void OnClickInfo(ItemSO data){
        switch (data.itemName){
            case "One More Chance": 
                GameManager.Instance.AddLife();
                break;
            case "Become Bigger":
                player.GetComponent<Player>().Grow();
                break;
            case "Final Reserves":
                GameManager.Instance.ZeroCoin(data);
                player.GetComponent<Player>().Starpower(10f);
                break;
            case "Just One":
                GameManager.Instance.AddCoin();
                break;
            case "Blood Money":
                if (GameManager.Instance.lives < 3) {
                    errorText.text = "Your life is lower than 3!";
                    return;
                }
                else { 
                    GameManager.Instance.BloodMoney();
                    break;
                }
            case "Caretaker's Chosen":
                GameManager.Instance.Chosen();
                break;
            case "Risky Moves":
                GameManager.Instance.Random();
                break;
            default: break;
        }
        //GameManager.Instance.coins -= data.cost;
        if(GameManager.Instance.coins > 0)
        {
            GameManager.Instance.DeleteCoin(data);
        }
        ResetList();
        ClearSpawnItemList();
        ClosePanel();
        GameManager.Instance.NextLevel();
    }

    public void OpenPanel(){
        panel.SetActive(true);
        open = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void ClosePanel(){
        panel.SetActive(false);
        open = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
