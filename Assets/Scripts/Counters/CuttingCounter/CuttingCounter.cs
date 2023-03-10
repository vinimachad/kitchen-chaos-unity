using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : ProgressBarCounter
{

    public event EventHandler OnCuttingItem;
    public override event EventHandler<float> OnShowProgressBar;
    public override event EventHandler OnHideProgressBar;

    [SerializeField] private CuttingRecipesSO[] cuttingRecipesSO;
    private int cuttedTimes = 0;

    public override void Interact(Player player)
    {
        if (player.GetPlayerPickedItem().HasItem())
        {
            if (HasItem()) return;

            if (CheckIfCanCutItem(player.GetPlayerPickedItem().Item))
                player.PlaceItemOn(this);
        }
        else
        {
            if (HasItem())
                player.PickItem(Item);
            else return;
        }
    }

    public override void UtilitiesInteract(Player player, bool isHolding)
    {
        if (HasItem())
        {
            if (player.GetPlayerPickedItem().HasItem()) return;
            else
            {
                CheckIfCanSpawnCuttedItem(isHolding);
            }
        }
    }

    #region Cut Logic Region

    private bool CheckIfCanCutItem(KitchenObject item)
    {
        var recipe = GetCurrentRecipe(item);
        if (recipe != null)
        {
            return true;
        }

        return false;
    }

    private void CheckIfCanSpawnCuttedItem(bool isHolding)
    {
        var recipe = GetCurrentRecipe(Item);
        if (recipe && isHolding)
        {

            StartCoroutine(WaitAndPrint(recipe, () =>
            {
                Item.DestroyYourSelf();
                KitchenObject.InstantiateItemAndPassTo(recipe.output.prefab, counterTopPoint, this);
                OnHideProgressBar?.Invoke(this, EventArgs.Empty);
            }));

        }
        else
        {
            StopAllCoroutines();
            cuttedTimes = 0;
            OnHideProgressBar?.Invoke(this, EventArgs.Empty);
        }
    }

    private CuttingRecipesSO GetCurrentRecipe(KitchenObject item)
    {
        foreach (var cuttingRecipe in cuttingRecipesSO)
        {
            if (cuttingRecipe.input == item.GetKitchenObjectSO())
            {
                return cuttingRecipe;
            }
        }

        return null;
    }
    #endregion


    private IEnumerator WaitAndPrint(CuttingRecipesSO recipe, Action onFinishCutting)
    {
        foreach (var value in Enumerable.Range(0, recipe.maxCutTime))
        {
            yield return new WaitForSeconds(.5f);
            cuttedTimes++;
            OnCuttingItem?.Invoke(this, EventArgs.Empty);
            float progressBarAmount = (float)cuttedTimes / recipe.maxCutTime;
            OnShowProgressBar?.Invoke(this, progressBarAmount);

            if (cuttedTimes == recipe.maxCutTime)
                onFinishCutting.Invoke();
        }
    }
}
