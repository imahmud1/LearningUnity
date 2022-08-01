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
    private Vector2 healthBarOrigSize;
    public int attackPower = 25;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
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



    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    void Start()
    {
        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";
        SetSpawnPosition();
        
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

    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("Spawn Location").transform.position;
    }

    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackBox.SetActive(false);
    }

    public void UpdateUI()
    {
        GameManager.Instance.coinsText.text = "Coins: " + coinsCollected.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
