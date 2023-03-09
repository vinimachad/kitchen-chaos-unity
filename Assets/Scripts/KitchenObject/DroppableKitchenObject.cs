using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableKitchenObject : MonoBehaviour, ISelectable
{

    [SerializeField] private KitchenObject kitchenObject;

    static public void SpawnDroppableKitchenObjectInPos(Transform pos, KitchenObject item)
    {
        Transform droppablePrefab = item.GetKitchenObjectSO().droppable;
        Instantiate(droppablePrefab, pos.position, pos.rotation);
        item.DestroyYourSelf();
    }

    public void Interact(Player player)
    {
        if (player.GetPlayerPickedItem().HasItem()) return;
        KitchenObject.InstantiateItemAndPassTo(kitchenObject.GetKitchenObjectSO().prefab, player.GetPlayerPickedItem().transform, player.GetPlayerPickedItem());
        gameObject.GetComponentInChildren<SelectCounter>().Hide();
        DestroyYourSelf();
    }

    public void UtilitiesInteract(Player player, bool isHolding)
    {
        Debug.Log("Utilities Interacting");
    }

    public void DestroyYourSelf()
    {
        Destroy(gameObject);
    }

}
