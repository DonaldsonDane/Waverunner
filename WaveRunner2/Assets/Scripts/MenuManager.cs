using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class MenuManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup settingsCG;
    [SerializeField] private CanvasGroup mainCG;
    [SerializeField] private CanvasGroup shipSelectionCG;
    [SerializeField] private CanvasGroup enemyChooseCG;

    [SerializeField] private GameManager gm;


    [SerializeField] private Transform shipImageHolderTransform;
    [SerializeField] private Transform shipPrefabsTransform;

    private int shipIndex;


    [SerializeField] private GameObject nextShipButton;
    [SerializeField] private GameObject prievShipButton;


    [SerializeField] TMP_Text shipName;

    [SerializeField] private Animator loadingScreenAnim;


    private void Start()
    {
       
    }

    private void Update()
    {
        switch (shipIndex)
        {
            //The Classic Cleanup
            case 0:
                shipName.text = "The Classic Cleanup";
                break;
            //The Light Looper
            case 1:
                shipName.text = "The Light Looper";
                break;
            //The Palm Pusher
            case 2:
                shipName.text = "The Palm Pusher";
                break;
            //The Racing Roger
            case 3:
                shipName.text = "The Racing Roger";
                break;
            //The Simple Straifer
            case 4:
                shipName.text = "The Simple Straifer";
                break;
            //The Wonky Whizzer
            case 5:
                shipName.text = "The Wonky Whizzer";
                break;
        }

        gm.playerShipId = shipIndex;
    }
    public void MenuButtonClicked(int id)
    {
        switch (id)
        {
            // This is the play button
            case 0:
                ShipSelection();
                break;
            // This is the settings button
            case 1:
                Settings();
                break;
            // This is the quit button
            case 2:
                QuitApplication();
                break;
            // Return from Ship Selection
            case 3:
                Return();
                break;
            case 4:
                EnemyConfig();
                break;
            case 5:
                ReturnToShipSelection();
                break;
            case 6:
                BeginGame();
                break;

        }

    }

    private void QuitApplication() => Application.Quit();
    private void HideCanvasGroup(CanvasGroup group)
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    private void ShowCanvasGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }


    protected bool settingsOpen = false;
    private void Settings()
    {
        settingsOpen = !settingsOpen;
        switch (settingsOpen)
        {
            case true:
                ShowCanvasGroup(settingsCG);
                HideCanvasGroup(mainCG);
              
                break;
                case false:
                ShowCanvasGroup(mainCG);
                HideCanvasGroup(settingsCG);
               
                break;
             
           
        }
    }


    private void BeginGame()
    {
        loadingScreenAnim.SetTrigger("Reveal");
    }

    private void ShipSelection()
    {
        shipIndex = 0;
        ShowCanvasGroup(shipSelectionCG);
        HideCanvasGroup(mainCG);
    }

    private void Return()
    {
        shipIndex = 0;
        ShowCanvasGroup(mainCG);
        HideCanvasGroup(shipSelectionCG);
        HideCanvasGroup(settingsCG);
    }

    private void EnemyConfig()
    {
        HideCanvasGroup(mainCG);
        HideCanvasGroup(shipSelectionCG);
        ShowCanvasGroup(enemyChooseCG);
    }

    private void ReturnToShipSelection()
    {
        HideCanvasGroup(mainCG);
        HideCanvasGroup(enemyChooseCG);
        ShowCanvasGroup(shipSelectionCG);
    }

    public void NextShip()
    {
        ShowNextShip();
    }

    public void PrevShip()
    {
        ShowPrievShip();
    }


    [SerializeField] private TMP_Dropdown difficultyDropdown;

    public void UpdateDifficulty()
    {
        switch (difficultyDropdown.value)
        {
            //I play Candy Crush
            case 0:
                gm.difficultyCount = GameManager.DifficultyCount.Easy;
                break;
            // I play Minecraft
            case 1:
                gm.difficultyCount = GameManager.DifficultyCount.Medium;
                break;
            //Discord is my main source of comms
            case 2:
                gm.difficultyCount = GameManager.DifficultyCount.Hard;
                break;
        }
    }

    [SerializeField] private TMP_Dropdown enemyDropdown;
    public void UpdateEnemyCount()
    {
        //We have to add 1, because of array index
        gm.enemyCount = enemyDropdown.value + 1; 
     
    }



    private void ShowNextShip()
    {
        Debug.Log("Next");
   
        if(shipIndex <= 4)
        {
            // Use a DOTween sequence to ensure the animation completes properly.
            Sequence moveSequence = DOTween.Sequence();

            shipImageHolderTransform.DOLocalMoveX((shipImageHolderTransform.localPosition.x -600), 0); // Adjust the X value as needed
            shipPrefabsTransform.DOLocalMoveX((shipPrefabsTransform.localPosition.x + 600), 0); // Adjust the X value as needed
            // Start the sequence.
            moveSequence.Play();
            shipIndex++;
        }


        //If ship index is 6, they can't go next anymore
        if(shipIndex == 5)
        {
            nextShipButton.gameObject.SetActive(false);
        }


        if(shipIndex >= 0)
        {
            prievShipButton.gameObject.SetActive(true);
        }
    }

    private void ShowPrievShip()
    {
        if(shipIndex >= 0)
        {
            // Use a DOTween sequence to ensure the animation completes properly.
            Sequence moveSequence = DOTween.Sequence();

            shipImageHolderTransform.DOLocalMoveX((shipImageHolderTransform.localPosition.x + 600), 0); // Adjust the X value as needed
            shipPrefabsTransform.DOLocalMoveX((shipPrefabsTransform.localPosition.x - 600), 0); // Adjust the X value as needed
            // Start the sequence.
            moveSequence.Play();
            shipIndex--;
        }

        if(shipIndex == 0)
        {
            prievShipButton.gameObject.SetActive(false);
        }

        if (shipIndex <= 5)
        {
            nextShipButton.gameObject.SetActive(true);
        }
    }





}
