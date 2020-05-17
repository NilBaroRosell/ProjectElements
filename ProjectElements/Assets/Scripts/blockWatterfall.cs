using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockWatterfall : MonoBehaviour
{
    [SerializeField] ParticleSystem watterfall;
    public void blockWatter()
    {
        watterfall.startLifetime = 0.8f;
        watterfall.transform.parent.GetChild(1).GetComponent<Collider>().enabled = false;
    }
}
