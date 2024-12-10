using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CookingInput : MonoBehaviour
{



    public RecipeManager recipeManager;

    // List to keep track of ingredients currently in the pot
    private List<Ingredient> ingredientsInPot = new List<Ingredient>();

    // Reference to the AudioSource component for playing sounds
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // If RecipeManager is not assigned in the inspector, try to find it
        if (recipeManager == null)
        {
            recipeManager = FindObjectOfType<RecipeManager>();
        }
    }


    // This function is called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an IngredientObject
        IngredientObject ingredientObject = other.GetComponent<IngredientObject>();
        if (ingredientObject != null)
        {
            // Add the ingredient to the list
            ingredientsInPot.Add(ingredientObject.ingredientID);
            Debug.Log("Ingredient added: " + ingredientObject.ingredientID.name);

            // Optionally, you can play a sound or visual effect here
        }
    }

    // This function is called when another collider exits the trigger collider attached to this object
    void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to an IngredientObject
        IngredientObject ingredientObject = other.GetComponent<IngredientObject>();
        if (ingredientObject != null)
        {
            // Remove the ingredient from the list
            ingredientsInPot.Remove(ingredientObject.ingredientID);
            Debug.Log("Ingredient removed: " + ingredientObject.ingredientID.name);
        }
    }

    [ContextMenu("Cook")]
    // Call this function when you want to process the ingredients in the pot
    public void CookIngredients()
    {
        if (ingredientsInPot.Count > 0)
        {
            // Convert the list to an array and pass it to the RecipeManager
            recipeManager.SearchRecipesCombo(ingredientsInPot.ToArray());
        }
        else
        {
            Debug.Log("No ingredients in the pot to cook.");
        }
    }

    [ContextMenu("Clear Pot")]
    // Optional: Function to clear the pot
    public void ClearPot()
    {
        ingredientsInPot.Clear();
        Debug.Log("Pot cleared.");
    }

    // Function to display or handle the story result
    public void DisplayStory(StoryResult recipe)
    {
        // Play the audio clip if available
        if (audioSource != null && recipe.clip != null)
        {
            audioSource.PlayOneShot(recipe.clip);
        }
        else
        {
            Debug.LogWarning("No AudioSource or AudioClip found.");
        }

        // Display the story name or any other relevant information
        Debug.Log("Story Unlocked: " + recipe.storyName);

        // Optionally, you can implement UI updates or other effects here
    }
}
