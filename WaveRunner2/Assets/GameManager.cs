using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Singleton instance
    private static GameManager _instance;

    //Holds what ship the player has chosen
    public int playerShipId = 0;

    //Holds how many enemies the player wants
    //Default to 1 to avoid any issues
    public int enemyCount = 1;

    public enum DifficultyCount
    {
        Easy,
        Medium,
        Hard
    }

    public DifficultyCount difficultyCount;

    private static int id;
    private static int count;
    private static DifficultyCount difficulty;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // If no instance exists, find it in the scene
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    // If it's still not found, create an empty GameObject and attach the GameManager script to it
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void InitializeGame()
    {
        id = playerShipId;
        count = enemyCount;
        difficulty = difficultyCount;
    }
}
