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
        transform.position = transform.parent.position + new Vector3(initialLocalPos.x, initialLocalPos.y + initialLocalPos.y * (Input.GetAxis("Vertical") * 2.0f), initialLocalPos.z);
    }
}
