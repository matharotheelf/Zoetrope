using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoetropeSpinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Vector3 rotationDirection;
    [SerializeField] Animator zoetropeAnimator;

    // Update is called once per frame
    void Update()
    {
        if (zoetropeAnimator.GetBool("ZoetropeSpinning"))
        {
            // rotate the point of origin of the zoetrope, this is used to track the angle of the zoetrope for the flashlight
            rotationPoint.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);

            // rotate the zoetrope around the rotation point
            transform.RotateAround(rotationPoint.position, rotationDirection, rotationSpeed * Time.deltaTime);
        }
    }
}
