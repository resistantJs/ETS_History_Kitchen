using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public StoryResult[] possibleRecipes;
    public CookingInput cookingInput;

    void Start()
    {
        // If CookingInput is not assigned, try to find it
        if (cookingInput == null)
        {
            cookingInput = FindObjectOfType<CookingInput>();
        }
    }

    public void SearchRecipesCombo(Ingredient[] inPot)
    {
        foreach (StoryResult recipe in possibleRecipes)
        {
            if (AreIngredientsMatching(recipe.ingredients, inPot))
            {
                // Match found
                // Output the story to CookingInput script or handle it as needed
                PlayStoryResult(recipe);
                break; // Exit loop if you want to process only the first match
            }
        }
    }

    private bool AreIngredientsMatching(Ingredient[] recipeIngredients, Ingredient[] inPotIngredients)
    {
        if (recipeIngredients.Length != inPotIngredients.Length)
            return false;

        // Create lists of ingredient names for comparison
        List<string> recipeIngredientNames = new List<string>();
        foreach (Ingredient ing in recipeIngredients)
        {
            recipeIngredientNames.Add(ing.name);
        }

        List<string> inPotIngredientNames = new List<string>();
        foreach (Ingredient ing in inPotIngredients)
        {
            inPotIngredientNames.Add(ing.name);
        }

        // Sort the lists to ensure order doesn't matter
        recipeIngredientNames.Sort();
        inPotIngredientNames.Sort();

        // Compare the sorted lists
        for (int i = 0; i < recipeIngredientNames.Count; i++)
        {
            if (recipeIngredientNames[i] != inPotIngredientNames[i])
                return false;
        }

        return true;
    }

    // private void PlayStoryResult(StoryResult recipe)
    // {
    //     // Assuming you have an AudioSource component on the same GameObject
    //     AudioSource audioSource = GetComponent<AudioSource>();
    //     if (audioSource != null && recipe.clip != null)
    //     {
    //         audioSource.PlayOneShot(recipe.clip);
    //     }
    //     else
    //     {
    //         Debug.LogError("AudioSource or AudioClip is missing!");
    //     }

    //     // You can also handle other outputs, such as displaying the story name
    //     Debug.Log("Story Unlocked: " + recipe.storyName);
    // }
    private void PlayStoryResult(StoryResult recipe)
    {
        // Call the DisplayStory function on the CookingInput script
        cookingInput.DisplayStory(recipe);
    }
}
