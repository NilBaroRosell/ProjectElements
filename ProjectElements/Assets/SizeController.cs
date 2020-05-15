using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    private float initialSize;
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        initialSize = gameObject.transform.localScale.x;
        size = initialSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemie")
        {
            size -= initialSize * 0.2f;
            if(size < 0)
            {
                size = 0;
                transform.localScale = new Vector3(size, size, size);
                // die
            }
            else
            {
                transform.localScale = new Vector3(size, size, size);
            }
        }

        if (collision.gameObject.tag == "Plant")
        {
            Destroy(collision.gameObject);
            size = initialSize;
            transform.localScale = new Vector3(size, size, size);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemie")
        {
            size -= initialSize * 0.2f;
            if (size < 0)
            {
                size = 0;
                // die
            }
            else
            {
                transform.localScale = new Vector3(size, size, size);
            }
        }
    }
}
