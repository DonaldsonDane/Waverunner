using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Holds what ship the player has chosen
    public int playerShipId = 0;

    private static int id;



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
    }
}
