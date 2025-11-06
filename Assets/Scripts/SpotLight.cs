using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour
{
    [SerializeField] float lightFrequency = 360f;
    [SerializeField] int holeCount = 13;
    [SerializeField] float flashAngleRange = 2f;
    [SerializeField] float spotlightRangeOn = 8f;
    [SerializeField] float spotlightRangeOff = 0f;
    [SerializeField] Transform zoetropeRotationPoint;
    [SerializeField] Animator zoetropeAnimator;
    [SerializeField] Light spotLight;

    private bool spotLightOn = false;


    void Update()
    {
        // only flash the spotlight if the zoetrope is spinning
        if (zoetropeAnimator.GetBool("ZoetropeSpinning"))
        {
            // if the zoetrop is at an angle so a gap is in the view of the player flash it on
            if (isZoetropeGapInView() && !spotLightOn)
            {
                // start coroutine to flash the light
                StartCoroutine(FlashLight());
            }
        }
    }

    bool isZoetropeGapInView()
    {
        // this checks wether the zoetrope has a gap in the players view using the angle range of a hole and also the number of holes of the zoetrope
        return zoetropeRotationPoint.rotation.y % holeCount >= -flashAngleRange / 2 && zoetropeRotationPoint.rotation.y % holeCount <= flashAngleRange / 2;
    }

    IEnumerator FlashLight()
    {
        // flash the light on for a period of time
        spotLightOn = true;
        spotLight.range = spotlightRangeOn;

        // wait the duration of one frequenct
        yield return new WaitForSeconds(1f / lightFrequency);

        // turn the light off
        spotLightOn = false;
        spotLight.range = spotlightRangeOff;
    }
}
