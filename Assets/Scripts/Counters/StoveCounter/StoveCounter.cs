using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : ProgressBarCounter
{

    enum State
    {
        Idle, Cooking, Cooked, Burned
    }

    public override event EventHandler<float> OnShowProgressBar;
    public override event EventHandler OnHideProgressBar;

    [SerializeField] private StoveRecipesSO[] stoveRecipesSO;

    private float cookCounter = 0;
    private State state;
    private StoveRecipesSO currentRecipe;
    private ProgressBarUI progressBar;

    private void Start()
    {
        state = State.Idle;
        progressBar = GetComponentInChildren<ProgressBarUI>();
    }

    private void Update()
    {
        if (HasItem())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Cooking:
                    CheckIfCanChangeCookStateTo(State.Cooked, currentRecipe);
                    break;
                case State.Cooked:
                    var recipe = CheckIfExistRecipeInStove(currentRecipe.output);
                    CheckIfCanChangeCookStateTo(State.Burned, recipe);
                    break;
                case State.Burned:
                    OnHideProgressBar?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }
        else
        {
            ClearCookProgress();
            OnHideProgressBar?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        PickItems playerPickedItem = player.GetPlayerPickedItem();
        bool playerHasItem = playerPickedItem.HasItem();
        KitchenObject playerKitchenObject = playerPickedItem.Item;

        if (playerHasItem)
        {
            if (!HasItem())
            {
                currentRecipe = CheckIfExistRecipeInStove(playerKitchenObject.GetKitchenObjectSO());
                bool recipeExistsInStove = currentRecipe != null;

                if (recipeExistsInStove)
                {
                    player.GetPlayerPickedItem().DropItemTo(this);
                    state = State.Cooking;
                }
            }
            else
            {
                CheckIfPlayerItemIsPlate(player);
            }
        }
        else
        {
            if (HasItem())
            {
                player.PickItem(GetItem());
            }
            else
            {
                CheckIfPlayerItemIsPlate(player);
            }
        }
    }

    private void ClearCookProgress()
    {
        cookCounter = 0;
    }

    private void CheckIfCanChangeCookStateTo(State newState, StoveRecipesSO recipe)
    {
        cookCounter += Time.deltaTime;
        OnShowProgressBar?.Invoke(this, cookCounter / recipe.timeToChangeState);
        if (cookCounter > recipe.timeToChangeState)
        {
            Item.DestroyYourSelf();
            KitchenObject.InstantiateItemAndPassTo(recipe.output.prefab, counterTopPoint, this);
            state = newState;
            cookCounter = 0;
        }
    }

    private StoveRecipesSO CheckIfExistRecipeInStove(KitchenSO item)
    {
        foreach (var recipe in stoveRecipesSO)
        {
            if (recipe.input == item)
            {
                return recipe;
            }
        }

        return null;
    }
}
