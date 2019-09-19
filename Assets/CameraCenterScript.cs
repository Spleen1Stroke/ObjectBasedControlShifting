using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterScript : MonoBehaviour
{
    // Start is called before the first frame update
    static public Vector3 controlModeRotation = new Vector3(45, 0, 0);
    static public Vector3 controlModePosition = new Vector3(0, 0, 0);
    //must have either rotation ignored or target ignored
    static public bool useControlRotation = false;
    static public bool useControlPosition = false;
    static public bool hasATarget = false;
    static public bool squareDeadSpace = false;
    static public bool radiusDeadSpace = false;
    static public bool teleport = false;

    static public float deadSpaceDistance = 0.5f;
    static public float movementSpeed = 10f;
    static public Vector3 rotationSpeed = new Vector3(60, 60, 60);

    Vector3 centerPosition;
    Vector3 centerRotation = new Vector3(0, 0, 0);
    Vector3 movementDirection;
    

    public static Transform target;
    public Transform defaultTarget;
    Vector3 test = new Vector3(4, 1, 3);
    void Start()
    {
        radiusDeadSpace = true;
        hasATarget = true;
        target = defaultTarget;
        
    }

    // Update is called once per frame
    void Update()
    {
        //predict the movement and rotation then actually move and stuff on the late
        RotateTheCenter();
        TeleportPositionTarget();
        SetMovementDirection();
        
    }

    void LateUpdate()
    {
        transform.position = centerPosition;
        transform.rotation = Quaternion.Euler(centerRotation);
    }

    void TeleportPositionTarget()
    {
        if (teleport == true && hasATarget == true) { centerPosition = target.position; }
        else if (teleport == true && useControlPosition == true) { centerPosition = controlModePosition; }
    }
    
    

    void SetMovementDirection()
    {
        float distance;
        if (hasATarget == true)
        {
            movementDirection = FindNotNormalDirection(centerPosition, target.position);
            distance = GetLengthOfVectors(movementDirection);
            
            if(squareDeadSpace == true)
            {
                if (Mathf.Abs(movementDirection.x) > deadSpaceDistance)
                {
                    if ((Mathf.Abs(movementDirection.x) - deadSpaceDistance) > (movementSpeed * Time.deltaTime))
                    {
                        if (movementDirection.x > 0) { centerPosition.x += movementSpeed * Time.deltaTime; }
                        if (movementDirection.x < 0) { centerPosition.x -= movementSpeed * Time.deltaTime; }
                    }
                    else if (((Mathf.Abs(movementDirection.x) - deadSpaceDistance) <= (movementSpeed * Time.deltaTime)) && deadSpaceDistance != 0)
                    {
                        if (movementDirection.x > 0) { centerPosition.x += movementDirection.x - deadSpaceDistance; }
                        if (movementDirection.x < 0) { centerPosition.x += movementDirection.x + deadSpaceDistance; }
                    }
                    else if ((Mathf.Abs(movementDirection.x) <= (movementSpeed * Time.deltaTime)) && deadSpaceDistance == 0)
                    {
                        if (movementDirection.x > 0) { centerPosition.x = target.position.x; }
                        if (movementDirection.x < 0) { centerPosition.x = target.position.x; }
                    }
                }

                if (Mathf.Abs(movementDirection.y) > deadSpaceDistance)
                {
                    if ((Mathf.Abs(movementDirection.y) - deadSpaceDistance) > (movementSpeed * Time.deltaTime))
                    {
                        if (movementDirection.y > 0) { centerPosition.y += movementSpeed * Time.deltaTime; }
                        if (movementDirection.y < 0) { centerPosition.y -= movementSpeed * Time.deltaTime; }
                    }
                    else if (((Mathf.Abs(movementDirection.y) - deadSpaceDistance) <= (movementSpeed * Time.deltaTime)) && deadSpaceDistance != 0)
                    {
                        if (movementDirection.y > 0) { centerPosition.y += movementDirection.y - deadSpaceDistance; }
                        if (movementDirection.y < 0) { centerPosition.y += movementDirection.y + deadSpaceDistance; }
                    }
                    else if ((Mathf.Abs(movementDirection.y) <= (movementSpeed * Time.deltaTime)) && deadSpaceDistance == 0)
                    {
                        if (movementDirection.y > 0) { centerPosition.y = target.position.y; }
                        if (movementDirection.y < 0) { centerPosition.y = target.position.y; }
                    }
                }

                if (Mathf.Abs(movementDirection.z) > deadSpaceDistance)
                {
                    if ((Mathf.Abs(movementDirection.z) - deadSpaceDistance) > (movementSpeed * Time.deltaTime))
                    {
                        if (movementDirection.z > 0) { centerPosition.z += movementSpeed * Time.deltaTime; }
                        if (movementDirection.z < 0) { centerPosition.z -= movementSpeed * Time.deltaTime; }
                    }
                    else if ((Mathf.Abs(movementDirection.z) - deadSpaceDistance) <= (movementSpeed * Time.deltaTime))
                    {
                        if (movementDirection.z > 0) { centerPosition.z += movementDirection.z - deadSpaceDistance; }
                        if (movementDirection.z < 0) { centerPosition.z += movementDirection.z + deadSpaceDistance; }
                    }
                }
            }
            
            if(radiusDeadSpace == true)
            {
                if (distance > deadSpaceDistance)
                {
                    if ((distance-deadSpaceDistance) > movementSpeed * Time.deltaTime)
                    {
                        centerPosition += movementDirection.normalized * movementSpeed * Time.deltaTime;
                    }
                    else if ((distance - deadSpaceDistance) <= movementSpeed * Time.deltaTime)
                    {
                        centerPosition += movementDirection.normalized * (distance - deadSpaceDistance);
                    }
                }
            }
        }


        if (useControlPosition == true)
        {
            movementDirection = FindNotNormalDirection(centerPosition, controlModePosition);
            distance = GetLengthOfVectors(movementDirection);
            if (distance > movementSpeed * Time.deltaTime) { centerPosition += movementDirection.normalized * movementSpeed * Time.deltaTime; }
            if (distance < movementSpeed * Time.deltaTime) { centerPosition = controlModePosition; }
        }
    }

    void RotateTheCenter()
    {
        if (useControlRotation == true)
        {
            controlModeRotation = FindSimplifiedEulerAnglesPositive(controlModeRotation);
            centerRotation = FindSimplifiedEulerAnglesPositive(centerRotation);
            //centerRotation = controlModeRotation;
            if (centerRotation.x != controlModeRotation.x)
            {
                if ((centerRotation.x - controlModeRotation.x) > 0 && (controlModeRotation.x - centerRotation.x) < 180)
                {
                    if (Mathf.Abs(controlModeRotation.x - centerRotation.x) <= rotationSpeed.x * Time.deltaTime) { centerRotation.x = controlModeRotation.x; }
                    else if (Mathf.Abs(controlModeRotation.x - centerRotation.x) > rotationSpeed.x * Time.deltaTime) { centerRotation.x -= rotationSpeed.x * Time.deltaTime; }
                }

                else if ((centerRotation.x - controlModeRotation.x) < 0 && (controlModeRotation.x - centerRotation.x) < 180)
                {
                    if (Mathf.Abs(controlModeRotation.x - centerRotation.x) <= rotationSpeed.x * Time.deltaTime) { centerRotation.x = controlModeRotation.x; }
                    else if (Mathf.Abs(controlModeRotation.x - centerRotation.x) > rotationSpeed.x * Time.deltaTime) { centerRotation.x += rotationSpeed.x * Time.deltaTime; }
                }
            

                else if ((centerRotation.x - controlModeRotation.x) > 0 && (controlModeRotation.x - centerRotation.x) > 180)
                {
                    if (Mathf.Abs(controlModeRotation.x - centerRotation.x) <= rotationSpeed.x * Time.deltaTime) { centerRotation.x = controlModeRotation.x; }
                    else if (Mathf.Abs(controlModeRotation.x - centerRotation.x) > rotationSpeed.x * Time.deltaTime) { centerRotation.x += rotationSpeed.x * Time.deltaTime; }
                }

                else if ((centerRotation.x - controlModeRotation.x) < 0 && (controlModeRotation.x - centerRotation.x) > 180)
                {
                    if (Mathf.Abs(controlModeRotation.x - centerRotation.x) <= rotationSpeed.x * Time.deltaTime) { centerRotation.x = controlModeRotation.x; }
                    else if (Mathf.Abs(controlModeRotation.x - centerRotation.x) > rotationSpeed.x * Time.deltaTime) { centerRotation.x -= rotationSpeed.x * Time.deltaTime; }
                }
            }

            if (centerRotation.y != controlModeRotation.y)
            {
                if ((centerRotation.y - controlModeRotation.y) > 0 && (controlModeRotation.y - centerRotation.y) < 180)
                {
                    if (Mathf.Abs(controlModeRotation.y - centerRotation.y) <= rotationSpeed.y * Time.deltaTime) { centerRotation.y = controlModeRotation.y; }
                    else if (Mathf.Abs(controlModeRotation.y - centerRotation.y) > rotationSpeed.y * Time.deltaTime) { centerRotation.y -= rotationSpeed.y * Time.deltaTime; }
                }

                else if ((centerRotation.y - controlModeRotation.y) < 0 && (controlModeRotation.y - centerRotation.y) < 180)
                {
                    if (Mathf.Abs(controlModeRotation.y - centerRotation.y) <= rotationSpeed.y * Time.deltaTime) { centerRotation.y = controlModeRotation.y; }
                    else if (Mathf.Abs(controlModeRotation.y - centerRotation.y) > rotationSpeed.y * Time.deltaTime) { centerRotation.y += rotationSpeed.y * Time.deltaTime; }
                }


                else if ((centerRotation.y - controlModeRotation.y) > 0 && (controlModeRotation.y - centerRotation.y) > 180)
                {
                    if (Mathf.Abs(controlModeRotation.y - centerRotation.y) <= rotationSpeed.y * Time.deltaTime) { centerRotation.y = controlModeRotation.y; }
                    else if (Mathf.Abs(controlModeRotation.y - centerRotation.y) > rotationSpeed.y * Time.deltaTime) { centerRotation.y += rotationSpeed.y * Time.deltaTime; }
                }

                else if ((centerRotation.y - controlModeRotation.y) < 0 && (controlModeRotation.y - centerRotation.y) > 180)
                {
                    if (Mathf.Abs(controlModeRotation.y - centerRotation.y) <= rotationSpeed.y * Time.deltaTime) { centerRotation.y = controlModeRotation.y; }
                    else if (Mathf.Abs(controlModeRotation.y - centerRotation.y) > rotationSpeed.y * Time.deltaTime) { centerRotation.y -= rotationSpeed.y * Time.deltaTime; }
                }
            }
        }
    }

    Vector3 FindNotNormalDirection(Vector3 point1, Vector3 point2) //this will not be normalized
    {
        Vector3 daReturn;
        daReturn.x = point2.x - point1.x;
        daReturn.y = point2.y - point1.y;
        daReturn.z = point2.z - point1.z;

        return daReturn;
    }
    


    float GetLengthOfVectors (Vector3 vectors)
    {
        float x = vectors.x;
        float y = vectors.y;
        float z = vectors.z;
        float length;

        length = Mathf.Sqrt((x * x) + (y * y) + (z * z));

        return length;
    }
    Vector3 FindSimplifiedEulerAnglesPositive (Vector3 angles) //returns a valuew between 0 and 360 
    {
        while(angles.x < 0)
        {
            angles.x += 360;
        }
        while (angles.x >= 360)
        {
            angles.x -= 360;
        }

       while (angles.y < 0)
        {
            angles.y += 360;
        }
        while (angles.y >= 360)
        {
            angles.y -= 360;
        }
        
        while (angles.z < 0)
        {
            angles.z += 360;
        }
        while(angles.z >= 360)
        {
            angles.z -= 360;
        }

        return angles;
    }
}
