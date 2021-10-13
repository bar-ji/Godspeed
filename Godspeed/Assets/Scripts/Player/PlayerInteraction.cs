using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private LayerMask layerMask;

    private bool isInteracting;
    private Transform t;
    RaycastHit r;

    private void Update()
    {
        if(isInteracting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            t.GetComponent<Raycastable>().OnClicked.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Raycastable raycastable;
        if (Physics.Raycast(camera.position, camera.forward, out r, 7.5f, layerMask))
        {
            if (r.transform.TryGetComponent(out raycastable) && !isInteracting)
            {
                t = r.transform;
                raycastable.OnInteracted.Invoke();
                isInteracting = true;
            }
        }
        else if(isInteracting)
        {
            t.GetComponent<Raycastable>().OnDeinteracted.Invoke();
            r = new RaycastHit();
            t = null;
            isInteracting = false;
        }

    }
}
