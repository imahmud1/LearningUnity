using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{

    enum ItemType { Coin, Ammo, Health, InventoryItem }
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if(itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += 2;
            }
            else if(itemType == ItemType.Health && NewPlayer.Instance.health < 100)
            {
                NewPlayer.Instance.health += 10;
            }
            else if(itemType == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            NewPlayer.Instance.UpdateUI();
            gameObject.SetActive(false);
        }
    }
}
