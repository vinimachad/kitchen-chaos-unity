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
        public ISelectable selectedCounter;
    }

    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    private ISelectable selectedCounter;
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
            DroppableKitchenObject.SpawnDroppableKitchenObjectInPos(playerPickedItem.transform, playerPickedItem.GetItem());
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
        HandlerInteraction();
    }

    private void HandlerInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, interactMask);
        float? lowerValue = null;
        Collider selectedCollider = null;

        if (colliders.Length == 0)
        {
            SetSelectedCounter(null);
        }

        foreach (var collider in colliders)
        {
            if (lowerValue == null)
            {
                lowerValue = Vector3.Distance(transform.position, collider.transform.position);
                selectedCollider = collider;
            }
            else
            {
                if (lowerValue > Vector3.Distance(transform.position, collider.transform.position))
                {
                    lowerValue = Vector3.Distance(transform.position, collider.transform.position);
                    selectedCollider = collider;
                }
            }
        }

        if (selectedCollider != null)
        {
            var selected = selectedCollider.transform.GetComponent<ISelectable>();
            SetSelectedCounter(selected);
            this.selectedCounter = selected;
        }
    }

    private void SetSelectedCounter(ISelectable selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedVisualCounterChanged?.Invoke(this, new SelectedVisualCounterArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
