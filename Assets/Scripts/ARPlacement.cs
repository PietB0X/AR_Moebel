using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private int countObjectPlaced = 0;
    private bool placementPoseIsValid = false;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {        

            if (countObjectPlaced < 3 && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ARPlaceObject();
                countObjectPlaced++;
            }

            UpdatePlacementPose();
            UpdatePlacementIndicator();
 
        



    }
    void UpdatePlacementIndicator()
    {
        //if (spawnedObject == null && placementPoseIsValid)
        //{
        if (countObjectPlaced > 2)
        {
            placementIndicator.SetActive(false);

        }
        else
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        
        //}
        //else
        //{
         //   placementIndicator.SetActive(false);
        //}
    }

    void UpdatePlacementPose()
    {
        if (Camera.current == null)
        {
            return;
        } 
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
       

    }


}
