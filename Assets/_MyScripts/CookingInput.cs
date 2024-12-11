using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class CookingInput : MonoBehaviour
{



    public RecipeManager recipeManager;

    // List to keep track of ingredients currently in the pot
    private List<Ingredient> ingredientsInPot = new List<Ingredient>();

    // Reference to the AudioSource component for playing sounds
    private AudioSource audioSource;
    
    // Reference the TextMeshPro game object
    public GameObject storyTextObject;

    // We'll store the TextMeshPro reference after we get it from the GameObject
    private TextMeshPro storyTextTMP;
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // If RecipeManager is not assigned in the inspector, try to find it
        if (recipeManager == null)
        {
            recipeManager = FindObjectOfType<RecipeManager>();
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
            storyTextTMP.SetText("Ingredient added: " + ingredientObject.ingredientID.name);
            return; // End here if we found an ingredient
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
            StopAllAudio();
            Debug.Log("Ingredient removed: " + ingredientObject.ingredientID.name);
            storyTextTMP.SetText("Ingredient removed: " + ingredientObject.ingredientID.name);
        }

        // If not an ingredient, check if it's the Mixer
        if (other.gameObject.CompareTag("Mixer"))
        {
            // The Mixer has entered the trigger - cook the ingredients
            CookIngredients();
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
        StopAllAudio();
        Debug.Log("Pot cleared.");
    }

    // Function to display or handle the story result
    public void DisplayStory(StoryResult recipe)
    {
        // Play the clip
        if (audioSource != null && recipe.clip != null)
        {
            //audioSource.PlayOneShot(recipe.clip);
            Debug.LogWarning("AudioPlayed");

        }
        else
        {
            Debug.LogWarning("No AudioSource or AudioClip found to play the story audio.");
        }
    }

    //Stop all sounds
    private AudioSource[] allAudioSources;

    void StopAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
    }
    // private void PlayStoryResult(StoryResult recipe)
    // {
    //     // Call the DisplayStory function on the CookingInput script
    //     cookingInput.DisplayStory(recipe);
    // }
}
