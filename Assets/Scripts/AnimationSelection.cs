using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelection : MonoBehaviour
{
    [SerializeField] Frame[] frames;
    [SerializeField] GameObject[] zoetropeFrames;
    [SerializeField] Frame selectedFrame;
    [SerializeField] Tutorial tutorial;

    public void SelectAnimation(Frame newAnimationFrame)
    {
        // This deselects the correct animation frame so the new one can be chosen
        DeselectAnimationFrame();

        // This updates the frames on the inner wall of the zoetrope with the new animation
        UpdateZoetropeFrames(newAnimationFrame);

        // This steps to the next step of the tutorial if the tutorial is at the correct stage
        ContinueTutorial();

        // Assign the new animation frame as selected
        selectedFrame = newAnimationFrame;
    }

    private void DeselectAnimationFrame()
    {
        // Sends a message to animation frame to deselect itself
        selectedFrame.Deselect();
    }

    private void ContinueTutorial()
    {
        // move to the next step of the tutorial if currently at artwork select
        if (tutorial.currentStage == Tutorial.Stage.ArtworkSelect)
        {
            tutorial.MoveToStage(Tutorial.Stage.LeverTurn);
        }
    }

    private void UpdateZoetropeFrames(Frame newAnimationFrame)
    {
        // loop through all the frames in the innerwall of the zoetrope and update with new animation frames
        for (int i = 0; i < zoetropeFrames.Length; i++)
        {
            GameObject zoetropeFrame = zoetropeFrames[i];
            zoetropeFrame.GetComponent<Renderer>().material = newAnimationFrame.animationMaterials[i];
        }
    }
}
