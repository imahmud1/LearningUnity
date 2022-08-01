using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text coinsText;
    public Image healthBar;
    public Image inventoryItemImage;









    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }



    private void Awake()
    {
        if (GameObject.Find("New Game Manager")) Destroy(gameObject);
        NewPlayer.Instance.SetSpawnPosition();
    }


    // Start is called before the first frame update
    void Start()
    {
        coinsText = GameObject.Find("Score").GetComponent<Text>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Game Manager";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
