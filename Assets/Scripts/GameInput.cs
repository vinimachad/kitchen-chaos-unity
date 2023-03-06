using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler<bool> OnHoldingUtilitiesInteract;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Interact.performed += Interact_performed;
        playerInput.Player.UtilitiesInteract.performed += UtilitiesInteract_performed;
        playerInput.Player.UtilitiesInteract.canceled += UtilitiesInteract_canceled;
    }

    private void UtilitiesInteract_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHoldingUtilitiesInteract?.Invoke(this, false);
    }

    private void UtilitiesInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHoldingUtilitiesInteract?.Invoke(this, true);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetPlayerInputNormalized()
    {
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();
        return input.normalized;
    }
}
