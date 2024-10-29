using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _trackedImageManager;
    [SerializeField] private GameObject _interactable;

    private IImageTrackingInteractable _interactionInterface;

    private List<ARTrackedImage> _trackedImages = new List<ARTrackedImage>();
    
    private void Awake()
    {
        // Sucht Componente auf dem Object wo dieses Script liegt
        // trackedImageManager = GetComponent<ARTrackedImageManager>();

        _interactionInterface = _interactable.GetComponent<IImageTrackingInteractable>();
    }

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagedChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagedChanged;
    }

    private void OnTrackedImagedChanged(ARTrackedImagesChangedEventArgs args)
    {
        // we use updated here as added spawns the TrackedImage on Origin and updated will update position later
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (!_trackedImages.Contains(trackedImage) || _trackedImages.Count < 1)
            {
                _trackedImages.Add(trackedImage);
                _interactionInterface.MakeVisibleAndSetPosition(trackedImage.transform.position);
            }
        }
        
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            _trackedImages.Remove(trackedImage);
        }
    }
/*
    void SetInteractionObjectPosition(Vector3 position)
    {
        Debug.Log($"Moving interactionObject: {interactionObject.transform} to {position.ToString()}.");
        interactionObject.transform.position = position;
    }
    
    /*
    
    private void SetCubePosition(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            cubeObject.transform.position = trackedImage.transform.position;
            cubeObject.transform.rotation = trackedImage.transform.rotation;
            cubeObject.SetActive(true);

            ARAnchor anchor = trackedImage.gameObject.AddComponent<ARAnchor>();
            cubeObject.transform.SetParent(anchor.transform);

            trackedImageManager.enabled = false;
        }
    }
    */
}
