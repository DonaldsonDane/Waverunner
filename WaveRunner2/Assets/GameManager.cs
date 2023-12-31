using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    // Singleton instance
    private static GameManager _instance;


    [SerializeField] private TMP_Text placeText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private CanvasGroup cg;
   

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




        StartCoroutine(OpeningSequence());
    }



    

    private IEnumerator OpeningSequence()
    {

      
        yield return new WaitForSeconds(5);

    }


    public void ShowEndGame()
    {
        placeText.text = GameObject.Find("RaceManager").gameObject.GetComponent<RaceManager>().playerPosition.ToString();
        timerText.text = GameObject.Find("Timerholder").gameObject.GetComponent<TimerController>().timer.ToString();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }


  
}
