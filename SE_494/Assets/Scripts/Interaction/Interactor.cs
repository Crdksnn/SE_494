using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int numFound;
    
    void Start()
    {
        
    }

    
    void Update()
    {
    
        //Holding interaction objects
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, _colliders, interactableMask);

        if (numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            
            //If player interactor object colllide with interactable objects and if player pressed "E" key from keyboard player will interact
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
    }

    //Draw interaction range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
