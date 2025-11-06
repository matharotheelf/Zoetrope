using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever: MonoBehaviour
{
    [SerializeField] Collider leverCollider;
    [SerializeField] Animator leverAnimator;
    [SerializeField] Animator zoetropeAnimator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float interactionRange = 3f;
    [SerializeField] Color activationColour = new Color(43, 43, 13);
    [SerializeField] Material leverMaterial;
    [SerializeField] Tutorial tutorial;

    private Ray _ray;
    private RaycastHit _hit;
    private bool _opened;
    private bool isCursorOver = false;
    private Color nonActivationColor = new Color(0, 0, 0);

    private void Update()
    {
        // set if the cursor is over the object
        isCursorOver = SetIsCursorOver();

        // if the activate button is pressed run the click function
        if (Input.GetButtonDown("Activate"))
        {
            Click();
        }

        // light up the lever so that user knows it can be activated 
        ActivationLightUp();
    }

    private void Click()
    {
        // activate the lever on click if the lever is in the correct state with the cursor over
        if(!leverAnimator.GetBool("LeverActivated") && isCursorOver)
        {
            // change the animation state
            leverAnimator.SetBool("LeverActivated", true);

            // move to the next stage of tutorial
            ContinueTutorial();
        }
    }

    private void ActivationLightUp()
    {
        if (isCursorOver)
        {
            // if the cursor is over the lever and in the correct state light it up
            if (!leverAnimator.GetBool("LeverActivated"))
            {
                leverMaterial.SetColor("_EmissionColor", activationColour);
                leverMaterial.EnableKeyword("_EMISSION");
            }
        } else
        {
            // if the cursor is not over the lever remove the lighting
            leverMaterial.SetColor("_EmissionColor", nonActivationColor);
            leverMaterial.EnableKeyword("_EMISSION");
        }
    }

    private bool SetIsCursorOver()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if raycast from mouse hits the object within the range return true
        if (Physics.Raycast(_ray, out _hit, interactionRange))
        {
            if (_hit.collider == leverCollider)
            {
                return true;
            }
        }

        return false;
    }

    private void ContinueTutorial()
    {
        // if the tutorial is not complete move to the final zoetrope view state
        if (tutorial.currentStage != Tutorial.Stage.Complete)
        {
            tutorial.MoveToStage(Tutorial.Stage.ZoetropeView);
        }
    }

    public void StartZoetrope()
    {
        // trigger the zoetrope spinning animation to start
        zoetropeAnimator.SetBool("ZoetropeSpinning", true);
    }

    public void playLeverSound()
    {
        // start the zoetrope moving sound
        audioSource.Play();
    }

    public void stopLeverSound()
    {
        // stop the zoetrope moving sound
        audioSource.Stop();
    }
}
