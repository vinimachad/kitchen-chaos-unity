using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    private Player player;
    private bool playerHasItem;
    private PickItems playerItem;

    public override void Interact(Player player)
    {

        this.player = player;
        playerItem = player.GetPlayerPickedItem();
        playerHasItem = playerItem.HasItem();

        if (HasItem())
            CheckIfIsPlate();
        else
            GrabbItemFromPlayer();
    }

    private void GrabbItemFromPlayer()
    {
        if (playerHasItem)
            player.PlaceItemOn(this);
    }

    private void PassItemToPlayer()
    {
        if (playerHasItem)
            CheckIfPlayerItemIsPlate(player);
        else
            player.PickItem(Item);
    }

    private void CheckIfIsPlate()
    {
        if (Item.CheckIfItemIsPlateKitchenObject())
            CheckIfItemWithPlayerIsRecipe();
        else
            PassItemToPlayer();
    }

    private void CheckIfItemWithPlayerIsRecipe()
    {
        if (playerHasItem)
        {
            PlateKitchenObject plateKitchenObject = Item as PlateKitchenObject;
            if (plateKitchenObject.TryAddKitchenObjetInPlate(playerItem.Item))
            {
                playerItem.Item.DestroyYourSelf();
            }
        }
        else
        {
            PassItemToPlayer();
        }
    }

    public override void SetItem(KitchenObject item)
    {
        base.SetItem(item);
    }
}
