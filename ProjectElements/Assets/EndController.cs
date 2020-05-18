using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "End")
        {
            if(SceneManager.GetActiveScene().name == "Sewer")
            {
                SceneManager.LoadScene("Nexo");
            }
            else if (SceneManager.GetActiveScene().name == "Nexo")
            {
                SceneManager.LoadScene("End");
            }
        }
    }
}
