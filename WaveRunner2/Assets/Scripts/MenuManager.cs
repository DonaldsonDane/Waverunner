using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup settingsCG;
    [SerializeField] private CanvasGroup mainCG;
    [SerializeField] private CanvasGroup shipSelectionCG;


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

    private void ShipSelection()
    {
        ShowCanvasGroup(shipSelectionCG);
        HideCanvasGroup(mainCG);
    }






}
