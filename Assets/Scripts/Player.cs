using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public event EventHandler<SelectedVisualCounterArgs> OnSelectedVisualCounterChanged;
    public class SelectedVisualCounterArgs: EventArgs
    {
       public ClearCounter selectedCounter;
    }
 
    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    private ClearCounter selectedCounter;

    [SerializeField] private float speed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private PickItems pickItem;

    private void Awake()
    {
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
    }

    public bool CheckIsWalking()
    {
        return isWalking;
    }

    public void PickItem(KitchenObject item)
    {
        pickItem.PickItem(item);
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
        HandleMovement();
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

        bool isInteracting = Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hit, interactionDistance);

        if (isInteracting)
        {
            bool hasClearCounter = hit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter);
            
            if (hasClearCounter)
            {
                SetSelectedCounter(clearCounter);
            } else
            {
                SetSelectedCounter(null);
            }

        } else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        float moveSpeed = speed * Time.deltaTime;
        Vector2 input = gameInput.GetPlayerInputNormalized();
        Vector3 moveDir = new(input.x, 0f, input.y);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, .6f, moveDir, moveSpeed);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, .6f, moveDirX, moveSpeed);

            if (canMove)
            {
                moveDir = moveDirX; 
            } else
            {

                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                 canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, .6f, moveDirZ, moveSpeed);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 15f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    } 
    

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedVisualCounterChanged?.Invoke(this, new SelectedVisualCounterArgs {
            selectedCounter = selectedCounter
        });
    }
}
