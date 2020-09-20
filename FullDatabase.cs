using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullDatabase : MonoBehaviour
{
    //list:dict:FoodObject
    //index for list is level requirement; 0 = fail or null
    //dictionary:  key(string of food's name ie. "sliced apples"):value(FoodObject)

    private List<Dictionary<string, FoodObject>> database { get; } //full database (all ingredients, given-recipes, hidden-recipes) -> immutable

    private List<Dictionary<string, FoodObject>> shopDatabase { get; } //all ingredients that can be bought from the shop -> immutable

    public List<Dictionary<string, FoodObject>> recipeDatabase { get; private set; } //all given-recipes -> mutable: hidden recipes can be added to this database when unlocked

    public List<Dictionary<string, FoodObject>> hiddenDatabase { get; private set; } //all hidden-recipes -> mutable: can be transfered to the recipeDatabase when unlocked

    //public List<Dictionary<string, FoodObject>> hiddenIngredientDatabase { get; } //all hidden-ingredients -> immutable (FUTURE UPDATE)

    public void transferRecipe(int index, string name)
    {
        recipeDatabase[index].Add(name, hiddenDatabase[index][name]); //adds new value into recipeDatabase from hiddenDatabase

        hiddenDatabase[index].Remove(name); //deletes old value from hiddenDatabase
    }

    public void BuildDatabase()
    {
        //builds full database
    }

    public void BuildShopDatabase()
    {
        //builds shop database
    }

    public void BuildRecipeDatabase()
    {
        //builds recipe database
    }

    public void BuildHiddenDatabase()
    {
        //builds hidden database
    }

    /*public void BuildHiddenIDatabase()
    {
        //FUTURE UPDATE
        //builds hidden ingredient database
    }*/
}
