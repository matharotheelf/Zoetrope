using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSelectionFrame : Frame
{
    [SerializeField] Color activationColour = new Color(43, 43, 13);
    [SerializeField] Material frameMaterial;

    private Color nonActivationColor = new Color(0, 0, 0);

    public void ActivationLightUp()
    {
        frameMaterial.SetColor("_EmissionColor", activationColour);
        frameMaterial.EnableKeyword("_EMISSION");
    }

    public void ActivationLightOff()
    {
        frameMaterial.SetColor("_EmissionColor", nonActivationColor);
        frameMaterial.EnableKeyword("_EMISSION");
    }
}
