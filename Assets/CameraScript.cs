using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3 (0, 0, -distance);
    }
    
}
