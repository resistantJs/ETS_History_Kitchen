using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EmptyStory", menuName = "HistoryKitchen/StoryResult", order = 1)]
public class StoryResult : ScriptableObject
{

    public string storyName;
    public Ingredient[] ingredients;

    public AudioClip clip;

}
