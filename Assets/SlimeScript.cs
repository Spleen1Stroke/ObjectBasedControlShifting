using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public static bool enableUpMvmnt = false;
    public static bool enableDownMvmnt = false;
    public static bool enableLeftMvmnt = false;
    public static bool enableRightMvmnt = false;
    public static Vector3 upVectors;
    public static Vector3 downVectors;
    public static Vector3 leftVectors;
    public static Vector3 rightVectors;

    int timesMovementUpdated = 0;
    Vector3 movement = new Vector3(0, 0, 0);
    float maxSpeedXZ = 3;
    float speedSide = 0;
    float speedUp = 0;
    float speed = 0;
    float accelleration = 30f;
    float deltaTime;

    bool wPressed;
    bool sPressed;
    bool dPressed;
    bool aPressed;
    bool speedAccelerated = false;


    Rigidbody slimePlayerRigidbody;
    Vector3 position;
    bool enabledButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        slimePlayerRigidbody = GetComponent<Rigidbody>();
        //print(movement);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //print(upVectors);
    }

    void FixedUpdate()
    {
        enabledButtonPressed = false;
        position = transform.position;
        updateSpeed();
        UpdateMovementVectors();
        LimitSpeed();
        MoveSlime();
        
    }


    void GetInput()
    {
        if (Input.GetKey(KeyCode.W) == true) { wPressed = true; }
        else { wPressed = false; }

        if (Input.GetKey(KeyCode.A) == true) { aPressed = true; }
        else { aPressed = false; }

        if (Input.GetKey(KeyCode.S) == true) { sPressed = true; }
        else { sPressed = false; }

        if (Input.GetKey(KeyCode.D) == true) { dPressed = true; }
        else { dPressed = false; }

    }

    void updateSpeed()
    {
        if (enableUpMvmnt == true)
        {
            if (wPressed && (sPressed == false)) { speedUp += accelleration * Time.fixedDeltaTime; }
        }

        if (enableDownMvmnt == true)
        {
            if (sPressed && (wPressed == false)) { speedUp -= accelleration * Time.fixedDeltaTime; }
        }

        if ((wPressed == false && sPressed == false) || (wPressed && sPressed))
        {
            if (speedUp > 0 && speedUp > accelleration * Time.fixedDeltaTime) { speedUp -= accelleration * Time.fixedDeltaTime; }
            if (speedUp < 0 && Mathf.Abs(speedUp) >accelleration * Time.fixedDeltaTime) { speedUp += accelleration * Time.fixedDeltaTime; }
            if (speedUp > 0 && speedUp < accelleration * Time.fixedDeltaTime) { speedUp = 0; }
            if (speedUp < 0 && Mathf.Abs(speedUp) < accelleration * Time.fixedDeltaTime) { speedUp = 0; }
        }
        
        if (enableUpMvmnt == false && speedUp > 0)
        {
            speedUp = 0;
        }

        if (enableDownMvmnt == false && speedUp < 0)
        {
            speedUp = 0;
        }
        //////////////////
        ////////////////////////
        ////////////////////// Horizontal_------------------
        if (enableRightMvmnt == true)
        {
            if (dPressed && (aPressed == false)) { speedSide += accelleration * Time.fixedDeltaTime; }
        }

        if (enableLeftMvmnt == true)
        {
            if (aPressed && (dPressed == false)) { speedSide -= accelleration * Time.fixedDeltaTime; }
        }

        if ((aPressed == false && dPressed == false) || (aPressed && dPressed))
        {
            if (speedSide > 0 && Mathf.Abs(speedSide) > accelleration * Time.fixedDeltaTime) { speedSide -= accelleration * Time.fixedDeltaTime; }
            if (speedSide < 0 && Mathf.Abs(speedSide) > accelleration * Time.fixedDeltaTime) { speedSide += accelleration * Time.fixedDeltaTime; }
            if (speedSide > 0 && Mathf.Abs(speedSide) < accelleration * Time.fixedDeltaTime) { speedSide = 0; }
            if (speedSide < 0 && Mathf.Abs(speedSide) < accelleration * Time.fixedDeltaTime) { speedSide = 0; }
        }

        if (enableRightMvmnt == false && speedSide > 0)
        {
            speedSide = 0;
        }
        if (enableLeftMvmnt == false && speedSide < 0)
        {
            speedSide = 0;
        }
    }


    void UpdateMovementVectors()
    {
        movement = new Vector3(0, 0, 0);

        if (enableUpMvmnt == true)
        {
            if (speedUp > 0) { movement += upVectors * speedUp * Time.fixedDeltaTime; }
        }
        if (enableDownMvmnt == true)
        {
            if (speedUp < 0) { movement += downVectors * Time.fixedDeltaTime * Mathf.Abs(speedUp); }
        }
        if (enableRightMvmnt == true)
        {
            if (speedSide > 0) { movement += rightVectors * Time.fixedDeltaTime * Mathf.Abs(speedSide); }
        }
        if (enableLeftMvmnt == true)
        {
            if (speedSide < 0) { movement += leftVectors * Time.fixedDeltaTime * Mathf.Abs(speedSide); }
        }
        //print(speedSide);
    }


    void LimitSpeed()
    {
        if (speedUp > maxSpeedXZ) { speedUp = maxSpeedXZ; }
        if (speedUp < -maxSpeedXZ) { speedUp = -maxSpeedXZ; }
        if (speedSide > maxSpeedXZ) { speedSide = maxSpeedXZ; }
        if (speedSide < -maxSpeedXZ) { speedSide = -maxSpeedXZ; }
    }

    void MoveSlime()
    {
        //if (enabledButtonPressed == true)//
        {

            position += movement;
            slimePlayerRigidbody.MovePosition(position);
            //print("moving");
        }
    }

}
