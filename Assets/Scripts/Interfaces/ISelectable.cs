using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public void UtilitiesInteract(Player player, bool isHolding);
    public void Interact(Player player);
}
