using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeController : MonoBehaviour
{
    private ParticleSystem ps;
    private Collider col;
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        col = transform.GetChild(1).GetComponent<Collider>();
        col.enabled = true;
        StartCoroutine(WaitForTime(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (col.enabled)
        {
            ps.Stop();
        }
        else
        {
            ps.Play();
        }
        StartCoroutine(ActivateCol(0.75f));
        StartCoroutine(WaitForTime(time));
    }

    private IEnumerator ActivateCol(float time)
    {
        yield return new WaitForSeconds(time);

        if (col.enabled)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
}
