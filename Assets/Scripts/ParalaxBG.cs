using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMult;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
        }
        else
        {
            Debug.LogError("Main Camera not found. Please ensure the main camera is tagged as 'MainCamera'.");
        }
    }

    private void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += deltaMovement * parallaxEffectMult;
            lastCameraPosition = cameraTransform.position;
        }
    }
}
