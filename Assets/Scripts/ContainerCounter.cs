using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnGrabbItem;
    [SerializeField] private KitchenSO kitchenScriptableObject;

    public override void Interact(Player player)
    {
        bool playerHasItem = player.GetPlayerPickedItem().HasItem();
        if (playerHasItem) 
          return;

        Transform kitchenObjecTransform = Instantiate(kitchenScriptableObject.prefab, counterTopPoint);
        kitchenObjecTransform.GetComponent<KitchenObject>().SetPickedPlace(this);
        player.PickItem(Item);
        OnGrabbItem?.Invoke(this, EventArgs.Empty);
    }
}
