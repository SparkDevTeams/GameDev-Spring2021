using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/IngredientScriptable")]
public class IngredientScriptable : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
}
