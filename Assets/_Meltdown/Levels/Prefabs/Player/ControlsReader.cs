using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls _controls;
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable(); 
    }

    public bool isJumping{ get; private set; }
    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.performed; 
    }

    public bool isDucking { get; private set; }

    public void OnDuck(InputAction.CallbackContext context)
    {
        isDucking = context.performed; 
    }

    public event Action onEscapeKeyEvent; 
    public void OnEscape(InputAction.CallbackContext context)
    {
        onEscapeKeyEvent?.Invoke();
    }
}
