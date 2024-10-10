using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadInputManager : MonoBehaviour
{

    [SerializeField] private InputActionProperty headposePositionInputAction;
    [SerializeField] private InputActionProperty headposeRotationInputAction;

    [Header("Object to control with head pose")] [SerializeField] private GameObject objectToControl;

    [SerializeField] private Vector3 headposeOffset;
    [SerializeField] private float smoothSpeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (headposePositionInputAction != null)
        {
            headposePositionInputAction.action.Enable();
            headposePositionInputAction.action.performed += PositionChanged;
        }

        if (headposeRotationInputAction != null)
        {
            headposeRotationInputAction.action.Enable();
            headposeRotationInputAction.action.performed += RotationChanged;
        }
    }

    private void PositionChanged(InputAction.CallbackContext obj)
    {
        var headposePosition = obj.ReadValue<Vector3>();
        objectToControl.transform.position = Vector3.Lerp(objectToControl.transform.position, headposePosition + headposeOffset, Time.deltaTime * smoothSpeed);
    }

    private void RotationChanged(InputAction.CallbackContext obj)
    {
        var headposeRotation = obj.ReadValue<Quaternion>();
        headposeRotation.y *= -1;
        headposeRotation.x *= -1;
        objectToControl.transform.rotation = Quaternion.Slerp(objectToControl.transform.rotation, headposeRotation, Time.deltaTime * smoothSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
