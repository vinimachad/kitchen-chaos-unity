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
        {
            player.PlaceItemOn(this);
            if (Item.CheckIfItemIsPlateKitchenObject())
            {
                CheckIfPlateAlreadyHasItem();
            }
        }
    }
    private void CheckIfIsPlate()
    {
        if (Item.CheckIfItemIsPlateKitchenObject())
        {
            CheckIfPlateAlreadyHasItem();
            CheckIfItemWithPlayerIsRecipe();
        }
        else
            PassItemToPlayer();
    }

    private void PassItemToPlayer()
    {
        if (playerHasItem)
            CheckIfPlayerItemIsPlate(player);
        else
            player.PickItem(Item);
    }


    private void CheckIfPlateAlreadyHasItem()
    {
        PlateKitchenObject plateKitchenObject = Item as PlateKitchenObject;

        if (plateKitchenObject.HasItem())
            plateKitchenObject.ShowRecipesUI();
        else
            plateKitchenObject.HideRecipesUI();
    }

    private void CheckIfItemWithPlayerIsRecipe()
    {
        if (playerHasItem)
        {
            PlateKitchenObject plateKitchenObject = Item as PlateKitchenObject;
            if (plateKitchenObject.TryAddKitchenObjetInPlate(playerItem.Item))
            {
                playerItem.Item.DestroyYourSelf();
                plateKitchenObject.ShowRecipesUI();
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
