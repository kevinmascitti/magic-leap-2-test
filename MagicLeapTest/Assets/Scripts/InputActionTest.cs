using UnityEngine;
using UnityEngine.InputSystem;
using Logger = LearnXR.Core.Logger;

/// <summary>
/// This script shows how to create an Input Action at runtime.
/// </summary>
public class InputActionTest : MonoBehaviour
{
    //The input action that will log it's Vector3 value every frame.
    [SerializeField]
    private InputAction positionInputAction = 
        new InputAction(binding:"<HandInteraction>{LeftHand}/pointerPosition", expectedControlType: "Vector3");

    //The input action that will log the grasp value.
    [SerializeField]
    private InputAction graspValueInputAction = 
        new InputAction(binding: "<HandInteraction>{LeftHand}/graspValue", expectedControlType: "Axis");

    // Input action for left hand pinch (Select action from XRI).
    [SerializeField]
    private InputAction pinchValueInputAction =
        new InputAction(binding: "<XRHandDevice>{LeftHand}/pinchValue", expectedControlType: "Axis");
    
    // Threshold for detecting a pinch gesture.
    private float pinchThreshold = 0.2f;
    
    private void Start()
    {
        positionInputAction.Enable();

        graspValueInputAction.Enable();
        
        pinchValueInputAction.Enable();
        
        // Register callback for pinch action.
        pinchValueInputAction.performed += OnPinchPerformed;
    }

    private void Update()
    {
        // Print pointer position to log.
        Logger.Instance.LogInfo($"Left Hand Pointer Position: {positionInputAction.ReadValue<Vector3>()}");

        // Print the left hand grasp value to log.
        Logger.Instance.LogInfo($"Left Hand Grasp Value: {graspValueInputAction.ReadValue<float>()}");
        
        Logger.Instance.LogInfo($"Left Hand Pinch Value: {pinchValueInputAction.ReadValue<float>()}");
    }
    
    // This function is called when the pinch action is performed.
    private void OnPinchPerformed(InputAction.CallbackContext context)
    {
        // Read the pinch value.
        float pinchValue = context.ReadValue<float>();

        // Check if the pinch value exceeds the threshold.
        if (pinchValue > pinchThreshold)
        {
            Logger.Instance.LogError("Pinch gesture detected with the left hand!");

            // You can add additional code here to send an event or trigger some action.
            // For example:
            // OnPinchEvent.Invoke();
        }
    }

    private void OnDestroy()
    {
        graspValueInputAction.Dispose();
        positionInputAction.Dispose();
        pinchValueInputAction.Dispose();
    }
}