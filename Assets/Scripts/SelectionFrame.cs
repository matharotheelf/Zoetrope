using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFrame : Frame
{
    [SerializeField] Collider selectionFrameCollider;
    [SerializeField] Color activationColour = new Color(43, 43, 13);
    [SerializeField] Material frameMaterial;
    [SerializeField] float interactionRange = 5f;

    private Ray _ray;
    private RaycastHit _hit;
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

        // light up the frame so that user knows it can be activated 
        ActivationLightUp();
    }

    private void Click()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // select the frame on click if the cursor is over
        if (isCursorOver) {
            Select();
        }
    }

    private void ActivationLightUp()
    {
        // if the cursor is over the frame or if it has already been selected light up the frame
        if (isCursorOver || isSelected)
        {
            frameMaterial.SetColor("_EmissionColor", activationColour);
            frameMaterial.EnableKeyword("_EMISSION");
        }
        // otherwise remove light
        else
        {
            frameMaterial.SetColor("_EmissionColor", nonActivationColor);
            frameMaterial.EnableKeyword("_EMISSION");
        }
    }

    private bool SetIsCursorOver()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // truen true if the raycast under the mouse hits the frame's collider
        if (Physics.Raycast(_ray, out _hit, interactionRange))
        {
            if (_hit.collider == selectionFrameCollider)
            {
                return true;
            }
        }

        return false;
    }
}
