using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class RecipeManager : MonoBehaviour
{
    public StoryResult[] possibleRecipes;
    public CookingInput cookingInput;

    // Reference to the AudioSource component for playing sounds
    private AudioSource audioSource;
    
    // Reference the TextMeshPro game object
    public GameObject storyTextObject;

    // We'll store the TextMeshPro reference after we get it from the GameObject
    private TextMeshPro storyTextTMP;
    void Start()
    {
        // If CookingInput is not assigned, try to find it
        if (cookingInput == null)
        {
            cookingInput = FindObjectOfType<CookingInput>();
        }
        
        // Ensure we get the TextMeshPro component from the assigned GameObject
        if (storyTextObject != null)
        {
            storyTextTMP = storyTextObject.GetComponent<TextMeshPro>();
            if (storyTextTMP == null)
            {
                Debug.LogWarning("No TextMeshPro component found on the assigned GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("No TextMeshPro GameObject assigned to storyTextObject.");
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
            else
            {
                Debug.Log("No Maching Recipe");
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

    private void PlayStoryResult(StoryResult recipe)
    {
        // Update the text
        if (storyTextTMP != null)
        {
            //storyTextTMP.text = recipe.storyText;
            storyTextTMP.SetText("Story: \n"+recipe.storyText);
        }
        else
        {
            Debug.Log("Story Unlocked: " + recipe.storyName + "\n" + recipe.storyText);
        }
        // Assuming you have an AudioSource component on the same GameObject
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && recipe.clip != null)
        {
            audioSource.PlayOneShot(recipe.clip);
            Debug.Log("Audio Played");
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is missing!");
        }

        // You can also handle other outputs, such as displaying the story name
        Debug.Log("Story Unlocked: " + recipe.storyName);
    }
    // private void PlayStoryResult(StoryResult recipe)
    // {
    //     // Call the DisplayStory function on the CookingInput script
    //     cookingInput.DisplayStory(recipe);
    // }

    
}
