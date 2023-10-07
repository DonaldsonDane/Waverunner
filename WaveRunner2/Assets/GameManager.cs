using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void InitializeGame()
    {
        id = playerShipId;
        count = enemyCount;
        difficulty = difficultyCount;
    }
}
