using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float jumpax = 1f;
    [SerializeField] private GameObject attackBox;
    public int health = 50;
    private int maxHealth = 100;
    public int coinsCollected = 0;
    public Text coinsText;
    public Image healthBar;
    private Vector2 healthBarOrigSize;
    public int attackPower = 25;

    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;
    public Sprite keySprite;
    public Sprite inventoryItemBlank;

    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<NewPlayer>();
            return instance;
        }
    }
    void Start()
    {
        coinsText = GameObject.Find("Score").GetComponent<Text>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;
        UpdateUI();
    }

    void Update()
    {
        //Movement
        targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0)* maxSpeed;
        
        //Left right change
        if(targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if(targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //Jump
        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpax;
        }

        //Game reset
        if(transform.position.y < -23f || health <= 0)
        {
            Die();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
    }

    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackBox.SetActive(false);
    }

    public void UpdateUI()
    {
        coinsText.text = "Coins: " + coinsCollected.ToString();
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), healthBar.rectTransform.sizeDelta.y);
    }

    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        inventoryItemImage.sprite = inventoryItemBlank;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
