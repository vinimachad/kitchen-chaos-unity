using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    private Player player;
    private bool playerHasItem;

    public override void Interact(Player player)
    {

        this.player = player;
        PickItems playerItem = player.GetPlayerPickedItem();
        playerHasItem = playerItem.HasItem();

        if (HasItem())
            PassItemToPlayer();
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
        if (playerHasItem) return;
        else
            player.PickItem(Item);
    }

    public override void SetItem(KitchenObject item)
    {
        base.SetItem(item);

    }
}
