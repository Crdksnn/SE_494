using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{

    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        
        sceneFader.FadeTo("Map Scene");
        return true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}