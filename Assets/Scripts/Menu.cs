using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour

{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject instructionsMenu;
    [SerializeField] GameObject playerFollowCamera;
    [SerializeField] GameObject tutorial;

    private void Start()
    {
        // unlock the cursor so user can click menu
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMainMenu()
    {
        // hide the main menu
        mainMenu.SetActive(false);

        // allow the player and camera to move
        playerFollowCamera.SetActive(true);

        // start the tutorial
        tutorial.SetActive(true);

        // lock the cursor so the user can turn fully
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowMainMenu()
    {
        // show the main main and stop the camera moving
        mainMenu.SetActive(true);
        playerFollowCamera.SetActive(false);
    }

    public void SwitchToControlsMenu()
    {
        // hide the main menu and show the controls
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void SwitchFromControlsMenu()
    {
        // hide the controls show the main menu
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void SwitchToInstructionsMenu()
    {
        // hide the main menu show the instructions
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void SwitchFromInstructionsMenu()
    {
        // hide the instructions show the main menu
        mainMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }
}