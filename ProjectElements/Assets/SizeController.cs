using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    private float initialSize;
    private float size;
    private float particleInitialLife;
    private float particleInitialSize;
    private float collisionRef = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        initialSize = gameObject.transform.parent.localScale.x;
        particleInitialLife = transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime;
        particleInitialSize = transform.GetChild(0).GetComponent<ParticleSystem>().startSize;
        size = initialSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collisionRef + 1.5f > Time.time) return;
        if (collision.gameObject.tag == "Enemie")
        {
            collisionRef = Time.time;
            size -= initialSize * 0.2f;
            transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime -= particleInitialLife * 0.2f;
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize -= particleInitialSize * 0.2f;
            if (size < 0)
            {
                size = 0;
                transform.parent.localScale = new Vector3(size, size, size);
                // die
            }
            else
            {
                transform.parent.localScale = new Vector3(size, size, size);
            }
        }

        if (collision.gameObject.tag == "Plant")
        {
            Destroy(collision.gameObject);
            size = initialSize;
            transform.parent.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.parent.localScale = new Vector3(size, size, size);
            transform.position = transform.parent.GetChild(0).position = new Vector3(collision.transform.position.x, collision.transform.position.y+1.0f, transform.parent.GetChild(0).position.z);
            transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime = particleInitialLife;
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize = particleInitialSize;   
        }
    }
}
