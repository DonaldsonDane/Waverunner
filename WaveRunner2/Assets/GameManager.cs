using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    // Singleton instance
    private static GameManager _instance;





    [SerializeField] private GameObject aiPathObject;
   

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
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
      
    }





    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);

        if (next.name == "MainScene")
        {
            BeginOpening();
        }



    }




    public void InitializeGame()
    {
        id = playerShipId;
        count = enemyCount;
        difficulty = difficultyCount;

    }


    private void BeginOpening()
    {






        Debug.Log("Beginning Starting Phase");

        //Set Variables



        //Make sure the AI can't move

        // Check if the reference to the GameObject is assigned
        if (aiPathObject != null)
        {
            // Get the AIPath component on the GameObject
            AIPath aiPath = aiPathObject.GetComponent<AIPath>();

            // Check if the AIPath component exists
            if (aiPath != null)
            {
                // Change the canMove property
                aiPath.canMove = false; // Set to true or false as needed
            }
            else
            {
                Debug.LogError("AIPath component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("Reference to the GameObject with AIPath script is not assigned.");
        }


        StartCoroutine(OpeningSequence());
    }



    [SerializeField] private Animator CountDownAnim;

    private IEnumerator OpeningSequence()
    {
        CountDownAnim.SetTrigger("Count");
        yield return null;
    }
}
