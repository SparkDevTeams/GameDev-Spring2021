using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CookingManager : MonoBehaviour
{
    [System.Serializable]
    public class Slot {
        public Image slotImg;
        public Button slotButton;
        public IngredientScriptable ingredientSO;
    };
    public List<Slot> slotList = new List<Slot>();
    public List<RecipeScriptable> recipeList = new List<RecipeScriptable>();
    
    [System.Serializable]
    public class ResultSlot {
        public Image slotImg;
        public Button slotButton;
        public string resultName;
    };
    public ResultSlot resultSlot;
    public move playerMove;

    public void TryAddIngredient(IngredientScriptable ingredient)
    {
        foreach (Slot slot in slotList)
        {
            if (!slot.ingredientSO)
            {
                bool haveItem = false;
                //Remove from inventory (if possible)
                switch (ingredient.ingredientName)
                {
                    case "BoarTrotter":
                        if (playerMove.boarTrotterCount > 0)
                        {
                            playerMove.boarTrotterCount -= 1;
                            playerMove.boarTrotterText.text = "Boar Trotter: " + playerMove.boarTrotterCount;
                            haveItem = true;
                        }                        
                        break;
                    case "Shroom":
                        if (playerMove.shroomCount > 0)
                        {
                            playerMove.shroomCount -= 1;
                            playerMove.shroomText.text = "Shroom: " + playerMove.shroomCount;
                            haveItem = true;
                        }
                        break;
                    default:
                        Debug.Log("Added ingredient with wrong name");
                        break;
                }

                if (haveItem)
                {
                    //Add to cooking menu
                    slot.ingredientSO = ingredient;
                    slot.slotButton.interactable = true;
                    slot.slotImg.enabled = true;
                    slot.slotImg.sprite = ingredient.ingredientSprite;

                    //Check recipes
                    List<IngredientScriptable> ingredients = new List<IngredientScriptable>();
                    foreach (Slot slot2 in slotList)
                    {
                        if (slot2.ingredientSO)
                        {
                            ingredients.Add(slot2.ingredientSO);
                        }
                    }

                    RecipeScriptable result = CheckRecipes(ingredients);
                    if (result)
                    {
                        switch (result.outputDishName)
                        {
                            case "BakKutTeh":
                                //Show BakKutTeh in resultSlot
                                resultSlot.resultName = result.outputDishName;
                                resultSlot.slotButton.interactable = true;
                                resultSlot.slotImg.enabled = true;
                                resultSlot.slotImg.sprite = result.outputDishSprite;
                                break;
                        }
                    }
                    else
                    {
                        resultSlot.slotButton.interactable = false;
                        resultSlot.slotImg.sprite = null;
                    }

                }                

                return;
            }
        }
    }

    public RecipeScriptable CheckRecipes(List<IngredientScriptable> ingredients)
    {
        foreach (RecipeScriptable recipe in recipeList)
        {
            bool isEqual = Enumerable.SequenceEqual(recipe.ingredientsList.OrderBy(e => e.ingredientName), ingredients.OrderBy(e => e.ingredientName));
            if (isEqual)
            {
                return recipe;
            }
        }

        return null;
    }

    public void CookDish()
    {
        switch (resultSlot.resultName)
        {
            case "BakKutTeh":
                //Add BakKutTeh here
                Debug.Log("Added BKT");
                break;
        }
    }
}
