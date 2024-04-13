using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Shooting : MonoBehaviour
{
    private InputSystemUIInputModule _inputModule;

    private void Start()
    {
        // Get the InputSystemUIInputModule component
        _inputModule = GetComponentInChildren<InputSystemUIInputModule>();
        print(_inputModule);

        // Subscribe to the left click event
        _inputModule.leftClick.action.performed += OnLeftClick;
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        Debug.Log("Left mouse button clicked!");


    }
}
