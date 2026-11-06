using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;   // slower than the player
    public float chaseRange = 6f;
    public float giveUpRange = 9f;    // longer than chaseRange
    public float arriveDistance = 0.2f;

    enum State { Patrol, Chase, Return }
    State state = State.Patrol;
    int index = 0;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Patrol:
                MoveTo(waypoints[index].position, patrolSpeed);
                if (Arrived(waypoints[index].position))
                    index = (index + 1) % waypoints.Length; // loop
                if (dist < chaseRange) state = State.Chase;
                break;

            case State.Chase:
                MoveTo(player.position, chaseSpeed);
                if (dist > giveUpRange) state = State.Return;
                break;

            case State.Return:
                MoveTo(waypoints[index].position, patrolSpeed);
                if (Arrived(waypoints[index].position)) state = State.Patrol;
                if (dist < chaseRange) state = State.Chase;
                break;
        }
    }

    void MoveTo(Vector3 target, float speed)
    {
        target.y = transform.position.y; // stay at the same height
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    bool Arrived(Vector3 target)
    {
        target.y = transform.position.y;
        return Vector3.Distance(transform.position, target) < arriveDistance;
    }
}