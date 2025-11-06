using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TMP_Text infoText;
    [SerializeField] static string artworkSelectMessage = "Select the artwork you wish to animate.";
    [SerializeField] static string leverTurnMessage = "Pull the lever to spin the zoetrope.";
    [SerializeField] static string zoetropeViewMessage = "Go up the stairs and look into the zoetrope.";

    public enum Stage
    {
        ArtworkSelect,
        LeverTurn,
        ZoetropeView,
        Complete
    }

    public Stage currentStage = Stage.ArtworkSelect;

    Dictionary<Stage, string> HelpTexts = new Dictionary<Stage, string>(){
        {Stage.ArtworkSelect, artworkSelectMessage},
        {Stage.LeverTurn, leverTurnMessage},
        {Stage.ZoetropeView, zoetropeViewMessage}
    };

    public void MoveToStage(Stage stage)
    {
        // move to the tutorial stage in the argument
        switch(stage)
        {
            case Stage.LeverTurn:
                // set the current stage to lever turn
                currentStage = Stage.LeverTurn;

                // set the info text to lever turn info text
                infoText.text = HelpTexts[Stage.LeverTurn];
                break;
            case Stage.ZoetropeView:
                // set the current stage to zoetrope view
                currentStage = Stage.ZoetropeView;

                // set the info text to zoetrope view info text
                infoText.text = HelpTexts[Stage.ZoetropeView];
                break;
            case Stage.Complete:
                // once the tutorial is complete hide the tutorial
                currentStage = Stage.Complete;
                infoText.gameObject.SetActive(false);
                break;
        }
    }
}
