using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoetropeAnimationContainer : MonoBehaviour
{
    [SerializeField] Animator leverAnimator;
    [SerializeField] Animator zoetropeAnimator;
    [SerializeField] float ambientLightIntensity = 1.2f;
    [SerializeField] float defaultSkyboxValue = 0.5f;
    [SerializeField] float defaultLightIntensity = 1.2f;
    [SerializeField] Material skyBox;
    [SerializeField] AudioSource gearsAudioSource;
    [SerializeField] AudioSource zoetropeAudioSource;
    [SerializeField] AudioClip zoetropeWinddownAudioClip;
    [SerializeField] AudioClip zoetropeSpinAudioClip;

    private void Start()
    {
        // set the skybox colour as default on application start
        Color newSkyColor = new Color(defaultSkyboxValue, defaultSkyboxValue, defaultSkyboxValue, defaultSkyboxValue);
        skyBox.SetColor("_Tint", newSkyColor);

        // set the ambient light intensity as default on application start
        RenderSettings.ambientIntensity = defaultLightIntensity;
    }

        private void Update()
    {
        // if zoetrope is spinning darken scene
        if (zoetropeAnimator.GetBool("ZoetropeSpinning"))
        {
            // darken the skybox
            Color newSkyColor = new Color(
                defaultSkyboxValue * ambientLightIntensity / defaultLightIntensity,
                defaultSkyboxValue * ambientLightIntensity / defaultLightIntensity,
                defaultSkyboxValue * ambientLightIntensity / defaultLightIntensity,
                defaultSkyboxValue
                );
            skyBox.SetColor("_Tint", newSkyColor);

            // darken the ambient light
            RenderSettings.ambientIntensity = ambientLightIntensity;
        }
    }

    private void OnApplicationQuit()
    {
        // set the skybox colour as default on application quit
        Color newSkyColor = new Color(defaultSkyboxValue, defaultSkyboxValue, defaultSkyboxValue, defaultSkyboxValue);
        skyBox.SetColor("_Tint", newSkyColor);

        // set the ambient light as default on application quit
        RenderSettings.ambientIntensity = defaultLightIntensity;
    }

    public void ResetZoetrope()
    {
        // return lever to unactivated state
        leverAnimator.SetBool("LeverActivated", false);

        // stop the zoetrope spinning
        zoetropeAnimator.SetBool("ZoetropeSpinning", false);
    }

    public void PlayZoetropeSoundEffects()
    {
        // play the gears spinning sound effects
        gearsAudioSource.Play();

        // set the zoetrope to the spinning sound effect and play
        zoetropeAudioSource.clip = zoetropeSpinAudioClip;
        zoetropeAudioSource.Play();
    }

    public void PlayZoetropeWindownSoundEffects()
    {
        // restart the zoetrope spinning audio with the winddown effect
        zoetropeAudioSource.Stop();
        zoetropeAudioSource.clip = zoetropeWinddownAudioClip;
        zoetropeAudioSource.Play();
    }

    public void StopZoetropeSoundEffects()
    {
        // stop all zoetrope sound effects
        gearsAudioSource.Stop();
        zoetropeAudioSource.Stop();
    }
}
