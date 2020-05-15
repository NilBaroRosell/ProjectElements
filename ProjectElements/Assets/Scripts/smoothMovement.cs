using UnityEngine;

public class smoothMovement : MonoBehaviour
{

    public GameObject Player;
    public float speed;
    private float initialSpeed;

    private void Start()
    {
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y) > 10) speed = initialSpeed * 2;
        else speed = initialSpeed;
        transform.position = Vector3.Lerp(transform.position, Player.transform.position - new Vector3(0, 0.5f), speed * Time.deltaTime);
        //Debug.Log(Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y));
    }
}
