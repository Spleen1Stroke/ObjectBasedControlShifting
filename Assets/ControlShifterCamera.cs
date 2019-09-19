using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShifterCamera : MonoBehaviour
{
    public bool useControlPosition;
    public Vector3 controlModePosition;
    public bool useControlRotation;
    public Vector3 controlModeRotation;
    public bool hasATarget;
    public Transform target;
    public bool squareDeadSpace;
    public bool radiusDeadSpace;
   
    [Tooltip("Teleport ignores movement speed and teleports center to its point or target")]
    public bool teleport; 

    
    public bool affectDeadSpaceDistance;
    public float deadSpaceDistance;
    public bool affectMovementSpeed;
    public float movementSpeed;
    public bool changeCameraDistance;
    public float cameraDistance;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "PlayerControlTrigger")
        {
            CameraCenterScript.controlModePosition = controlModePosition;
            CameraCenterScript.controlModeRotation = controlModeRotation;
            CameraCenterScript.useControlPosition = useControlPosition;
            CameraCenterScript.useControlRotation = useControlRotation;
            CameraCenterScript.hasATarget = hasATarget; //this will override useControlPosition
            CameraCenterScript.squareDeadSpace = squareDeadSpace;
            CameraCenterScript.radiusDeadSpace = radiusDeadSpace;
            CameraCenterScript.teleport = teleport;
            CameraCenterScript.target = target;
            if (affectDeadSpaceDistance == true) { CameraCenterScript.deadSpaceDistance = deadSpaceDistance; }
            if (affectMovementSpeed == true) { CameraCenterScript.movementSpeed = movementSpeed; }
            if (changeCameraDistance == true) { CameraScript.distance = cameraDistance; }
        }
    }

    void OnTriggerStay(Collider triggerCollider)
    {
        if (triggerCollider.tag == "PlayerControlTrigger")
        {
            
        }
    }
}
