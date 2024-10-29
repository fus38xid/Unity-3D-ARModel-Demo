using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackingInteractableCube : MonoBehaviour, IImageTrackingInteractable
{
    public void MakeVisibleAndSetPosition(Vector3 targetPosition)
    {
        Debug.Log($"old pos: {transform.position.ToString()} | new pos: {targetPosition.ToString()}");
        this.transform.position = targetPosition;
        this.transform.localScale *= 0.15f;
        this.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log($"actual pos: {transform.position.ToString()}");
    }
}
