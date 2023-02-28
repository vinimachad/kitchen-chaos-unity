using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropable
{
    public void DropItemTo(IPickUp pickableObject);
}
