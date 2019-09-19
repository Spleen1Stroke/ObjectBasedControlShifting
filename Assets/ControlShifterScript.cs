using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShifterScript : MonoBehaviour
{
    public bool enableUpMvmnt;
    public bool enableDownMvmnt;
    public bool enableLeftMvmnt;
    public bool enableRightMvmnt;
    public Vector3 upVectors; //need to be prenormalized so that the movement is perfect;
    public Vector3 downVectors;
    public Vector3 leftVectors;
    public Vector3 rightVectors;
    //public bool useVectors;
    //public float horizontalDirection; // in degress need to be converted to radians
    //public float verticalDirection;
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
            SlimeScript.enableUpMvmnt = enableUpMvmnt;
            SlimeScript.enableDownMvmnt = enableDownMvmnt;
            SlimeScript.enableLeftMvmnt = enableLeftMvmnt;
            SlimeScript.enableRightMvmnt = enableRightMvmnt;

            SlimeScript.upVectors = upVectors;
            SlimeScript.downVectors = downVectors;
            SlimeScript.leftVectors = leftVectors;
            SlimeScript.rightVectors = rightVectors;
        }
    }

    void OnTriggerStay(Collider triggerCollider)
    {
        if (triggerCollider.tag == "PlayerControlTrigger")
        {
            SlimeScript.enableUpMvmnt = enableUpMvmnt;
            SlimeScript.enableDownMvmnt = enableDownMvmnt;
            SlimeScript.enableLeftMvmnt = enableLeftMvmnt;
            SlimeScript.enableRightMvmnt = enableRightMvmnt;

            SlimeScript.upVectors = upVectors;
            SlimeScript.downVectors = downVectors;
            SlimeScript.leftVectors = leftVectors;
            SlimeScript.rightVectors = rightVectors;
        }
    }
}
