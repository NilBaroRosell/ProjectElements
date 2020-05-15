using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieblaScript : MonoBehaviour
{
    private Vector3 scrollSpeed = new Vector3(-0.05f, 0, 0);
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(48.48873f, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        //float newPos = Mathf.Repeat(Time.time * scrollSpeed, 35);
        //transform.position = startPos + Vector2.right * newPos;
        transform.position += scrollSpeed;
        if (transform.localPosition.x < -20) transform.localPosition = startPos;
    }
}
