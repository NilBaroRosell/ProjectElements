using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRefScript : MonoBehaviour
{
    Vector3 initialLocalPos;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = transform.localPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = initialLocalPos + new Vector3(0, initialLocalPos.y * Input.GetAxis("Vertical"), 0);
    }
}
