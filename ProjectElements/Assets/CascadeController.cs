using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeController : MonoBehaviour
{
    private ParticleSystem ps;
    private BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        col = transform.parent.transform.GetChild(1).GetComponent<BoxCollider2D>();
        col.enabled = true;
        StartCoroutine(WaitForTime(2));
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
            col.enabled = false;
        }
        else
        {
            ps.Play();
            col.enabled = true;
        }
        StartCoroutine(WaitForTime(2));
    }
}
