using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{

    [SerializeField] private List<KitchenSO> validKitchenSOs;
    [SerializeField] private List<PlateCompleteVisual> completeVisuals;
    [SerializeField] private Transform plateTopPoint;
    [SerializeField] private RecipesInPlateUI recipesInPlateUI;

    private List<KitchenSO> kitchenObjectsInPlate;

    public event EventHandler<KitchenSO> OnShowKitchenObjectInPlate;

    private void Awake()
    {
        kitchenObjectsInPlate = new List<KitchenSO>();
    }

    public PlateKitchenObject(KitchenSO kitchenSO)
    {
        this.kitchenSO = kitchenSO;
    }

    public bool TryAddKitchenObjetInPlate(KitchenObject kitchenObject)
    {
        KitchenSO kitchenSO = kitchenObject.GetKitchenObjectSO();

        if (!validKitchenSOs.Contains(kitchenSO)) return false;

        kitchenObjectsInPlate.Add(kitchenSO);
        ShowKitchenObjectInPlate(kitchenSO);
        return true;
    }

    private void ShowKitchenObjectInPlate(KitchenSO kitchenSO)
    {
        recipesInPlateUI.AddIconInGridLayout(kitchenSO.sprite);
        OnShowKitchenObjectInPlate?.Invoke(this, kitchenSO);
    }

    public void ShowRecipesUI()
    {
        recipesInPlateUI.Show();
    }

    public void HideRecipesUI()
    {
        recipesInPlateUI.Hide();
    }

    public bool HasItem()
    {
        return kitchenObjectsInPlate.Count != 0;
    }
}
