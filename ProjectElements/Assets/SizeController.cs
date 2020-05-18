using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    private float initialSize;
    private float initialRad;
    private float initialHeight;
    private float size;
    private float particleInitialLife;
    private float particleInitialSize;
    private float collisionRef = 0.0f;
    public Vector3 respawn;

    // Start is called before the first frame update
    void Start()
    {
        initialRad = transform.GetComponent<CapsuleCollider>().radius;
        initialHeight = transform.GetComponent<CapsuleCollider>().height;
        initialSize = gameObject.transform.parent.localScale.x;
        particleInitialLife = transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime;
        particleInitialSize = transform.GetChild(0).GetComponent<ParticleSystem>().startSize;
        size = initialSize;
        respawn = transform.parent.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            size = 0;
            transform.parent.localScale = new Vector3(size, size, size);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            size = 0;
            transform.parent.localScale = new Vector3(size, size, size);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collisionRef + 1.5f > Time.time) return;
        if (collision.gameObject.tag == "Enemie")
        {
            collisionRef = Time.time;
            size -= initialSize * 0.2f;
            GetComponent<CapsuleCollider>().radius = 
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().radius -= initialRad * 0.2f;
            GetComponent<CapsuleCollider>().height =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().height -= initialHeight * 0.2f;
            transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime -= particleInitialLife * 0.2f;
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize -= particleInitialSize * 0.2f;
            if (size <= 0.1f)
            {
                size = initialSize;
                transform.parent.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.parent.localScale = new Vector3(size, size, 1);
                transform.position = transform.parent.GetChild(0).position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.0f, transform.parent.GetChild(0).position.z);
                transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime = particleInitialLife;
                transform.GetChild(0).GetComponent<ParticleSystem>().startSize = particleInitialSize;
                GetComponent<CapsuleCollider>().radius =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().radius = initialRad;
                GetComponent<CapsuleCollider>().height =
                    transform.parent.GetChild(0).GetComponent<CapsuleCollider>().height = initialHeight;
                transform.parent.GetChild(0).position = transform.parent.GetChild(1).position = respawn;

                transform.parent.GetChild(0).GetComponent<Controller>().restartlife();
                GameObject.Find("Canvas").GetComponent<inventory>().updateLifes(-1);
            }
            else
            {
                Vector3 aux = transform.parent.GetChild(0).position;
                transform.parent.position = aux + Vector3.up * 3;
                transform.parent.GetChild(0).position = transform.parent.GetChild(1).position = aux;
                transform.parent.localScale = new Vector3(size, size, 1);
            }
            transform.GetChild(2).GetComponent<ParticleSystem>().Play(true);
        }

        if (collision.gameObject.tag == "Water")
        {
            size = initialSize;
            transform.parent.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.parent.localScale = new Vector3(size, size, 1);
            transform.position = transform.parent.GetChild(0).position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1.0f, transform.parent.GetChild(0).position.z);
            transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime = particleInitialLife;
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize = particleInitialSize;
            GetComponent<CapsuleCollider>().radius =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().radius = initialRad;
            GetComponent<CapsuleCollider>().height =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().height = initialHeight;
            transform.parent.GetChild(0).position = transform.parent.GetChild(1).position = respawn;
            transform.parent.GetChild(0).GetComponent<Controller>().restartlife();
            GameObject.Find("Canvas").GetComponent<inventory>().updateLifes(-1);
            transform.GetChild(2).GetComponent<ParticleSystem>().Play(true);
        }

        if (collision.gameObject.tag == "Plant")
        {
            Destroy(collision.gameObject);
            size = initialSize;
            transform.parent.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 aux = transform.parent.GetChild(0).position;
            transform.parent.position = aux;
            transform.parent.GetChild(0).position = transform.parent.GetChild(1).position = aux;
            transform.parent.localScale = new Vector3(size, size, 1);
            transform.position = transform.parent.GetChild(0).position = new Vector3(collision.transform.position.x, collision.transform.position.y+1.0f, transform.parent.GetChild(0).position.z);
            transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime = particleInitialLife;
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize = particleInitialSize;
            GetComponent<CapsuleCollider>().radius =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().radius = initialRad;
            GetComponent<CapsuleCollider>().height =
                transform.parent.GetChild(0).GetComponent<CapsuleCollider>().height = initialHeight;
            transform.GetChild(2).GetComponent<ParticleSystem>().Play(true);
        }
    }
}
