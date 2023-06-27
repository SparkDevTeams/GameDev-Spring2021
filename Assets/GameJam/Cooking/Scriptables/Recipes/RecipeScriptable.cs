using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeScriptable")]
public class RecipeScriptable : ScriptableObject
{
    public List<IngredientScriptable> ingredientsList;
    public string outputDishName;
    public Sprite outputDishSprite;
}
