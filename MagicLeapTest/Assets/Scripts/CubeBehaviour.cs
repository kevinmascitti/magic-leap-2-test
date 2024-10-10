using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using MagicLeap.OpenXR.Features.LocalizationMaps;
using Unity.VisualScripting;
using UnityEngine.XR.OpenXR.NativeTypes;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private Material otherMaterial;
    private MagicLeapLocalizationMapFeature localizationMapFeature = null;
    
    // Start is called before the first frame update
    void Start()
    {
        // Obtain the instance of the localization Map Feature
        localizationMapFeature = OpenXRSettings.Instance.GetFeature<MagicLeapLocalizationMapFeature>();
        // If it is not present return
        if (localizationMapFeature == null || !localizationMapFeature.enabled)
        {
            Debug.LogError("Magic Leap Localization Map Feature does not exists or is disabled.");
            return;
        }

        // Enable the localization Events
        XrResult result = localizationMapFeature.EnableLocalizationEvents(true);
        if (result != XrResult.Success)
        {
            Debug.LogError($"Failed to enable localization events with result: {result}");
            return;
        }

        MagicLeapLocalizationMapFeature.OnLocalizationChangedEvent += OnLocalizationChanged;
    }

    void OnLocalizationChanged(LocalizationEventData data)
    {
        Debug.Log($"Localization Status: {data.State}");
        if (data.State == LocalizationMapState.Localized)
        {
            Debug.Log($"Localized to space: {data.Map.Name}");
            GetComponent<Renderer>().material = otherMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LocalizeMap()
    {
        if (localizationMapFeature == null || !localizationMapFeature.enabled)
            return;
        string mapUUID = "0";
        var result = localizationMapFeature.RequestMapLocalization(mapUUID);
        Debug.Log($"Localize request result: {result}");
    }
}
