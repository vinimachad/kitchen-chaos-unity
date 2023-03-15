using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnDropItem;
    public event EventHandler OnInteract;
    public event EventHandler<bool> OnHoldingUtilitiesInteract;

    public Vector2 move;
    [SerializeField] private bool isMobileInput;

    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Interact.performed += Interact_performed;
        playerInput.Player.UtilitiesInteract.performed += UtilitiesInteract_performed;
        playerInput.Player.UtilitiesInteract.canceled += UtilitiesInteract_canceled;
        playerInput.Player.DropItem.performed += DropItem_performed;
    }

    private void Update()
    {
        if (!isMobileInput)
        {
            SetMove(playerInput.Player.Move.ReadValue<Vector2>());
        }
    }

    private void DropItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDropItem?.Invoke(this, EventArgs.Empty);
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

    public void SetMove(Vector2 moveValue)
    {
        move = moveValue.normalized;
    }
}
