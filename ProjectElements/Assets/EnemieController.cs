using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    private Transform[] patrolPositions;
    [SerializeField] private int direction;
    [SerializeField] private int speed;
    [SerializeField] private float visionRange;
    [SerializeField] float attackSpeed = 0.03f;
    private enum State { PATROLLING, ATTACKING };
    private State state;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        patrolPositions = new Transform[transform.parent.transform.childCount - 1];
        for(int i = 0; i < patrolPositions.Length; i++)
        {
            patrolPositions[i] = transform.parent.transform.GetChild(i + 1).transform;
        }
        player = GameObject.Find("Player/Model").gameObject;
        state = State.PATROLLING;
        Physics.IgnoreLayerCollision(2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, player.transform.position.z);
        switch (state)
        {
        case State.PATROLLING:
            {
                transform.position = new Vector3(Vector3.MoveTowards(transform.position, patrolPositions[direction].position, Mathf.Abs((patrolPositions[0].position - patrolPositions[1].position).magnitude) / speed).x, transform.position.y, player.transform.position.z);
                if (Mathf.Abs((transform.position.x - patrolPositions[direction].position.x)) < 0.25f)
                {
                    direction = (direction == 1) ? 0 : 1;
                }
                if(Mathf.Abs(player.transform.position.x - transform.position.x) < visionRange && Mathf.Abs(player.transform.position.y - transform.position.y) < 1.75f)
                {
                    if ((player.transform.position.x < transform.position.x && direction == 0) || (player.transform.position.x > transform.position.x && direction == 1))
                    {
                        state = State.ATTACKING;
                    }
                }
                break;
            }
        case State.ATTACKING:
            {
                transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, attackSpeed).x, transform.position.y, player.transform.position.z);
                if (Mathf.Abs(player.transform.position.x - transform.position.x) > visionRange || Mathf.Abs(player.transform.position.y - transform.position.y) > 1.75f)
                {
                    state = State.PATROLLING;
                }
                break;
            }
        }
    }
}
