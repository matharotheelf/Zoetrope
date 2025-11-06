using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoetropeViewTrigger : MonoBehaviour
{
    [SerializeField] Tutorial tutorial;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            // complete the tutorial when the user is at the zoetrope viewing point
            ContinueTutorial();
        }
    }

    private void ContinueTutorial()
    {
        // if the tutorial is at zoetrope view stage complete
        if (tutorial.currentStage == Tutorial.Stage.ZoetropeView)
        {
            tutorial.MoveToStage(Tutorial.Stage.Complete);
        }
    }
}
