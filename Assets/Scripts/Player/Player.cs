using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public event EventHandler<SelectedVisualCounterArgs> OnSelectedVisualCounterChanged;
    public class SelectedVisualCounterArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    private PickItems playerPickedItem;

    [SerializeField] private float speed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactMask;

    private void Awake()
    {
        playerPickedItem = GetComponentInChildren<PickItems>();
        if (Instance != null)
        {
            Debug.Log("Already exist instace of player");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
        gameInput.OnHoldingUtilitiesInteract += GameInput_OnUtilitiesInteract;
        gameInput.OnDropItem += GameInput_OnDropItem;
    }

    private void GameInput_OnDropItem(object sender, System.EventArgs e)
    {
        if (playerPickedItem.HasItem())
        {

        }
    }

    private void GameInput_OnUtilitiesInteract(object sender, bool isHolding)
    {
        if (selectedCounter != null)
        {
            selectedCounter.UtilitiesInteract(this, isHolding);
        }
    }

    public bool CheckIsWalking()
    {
        return isWalking;
    }

    public void PickItem(KitchenObject item)
    {
        playerPickedItem.PickItem(item);
    }

    public void PlaceItemOn(IPickUp pickableObject)
    {
        playerPickedItem.DropItemTo(pickableObject);
    }

    public PickItems GetPlayerPickedItem()
    {
        return playerPickedItem;
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 input = gameInput.GetPlayerInputNormalized();
        Vector3 moveDir = new(input.x, 0f, input.y);
        float interactionDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }

        bool isInteracting = Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hit, interactionDistance, interactMask);

        if (isInteracting)
        {
            bool hasClearCounter = hit.transform.TryGetComponent(out BaseCounter counter);

            if (hasClearCounter)
            {
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedVisualCounterChanged?.Invoke(this, new SelectedVisualCounterArgs
        {
            selectedCounter = selectedCounter
        });
    }


}
