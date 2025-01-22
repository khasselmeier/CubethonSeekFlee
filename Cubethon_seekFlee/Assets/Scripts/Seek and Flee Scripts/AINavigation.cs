using UnityEngine;

public class AINavigation : MonoBehaviour
{
    public enum AIState
    {
        Seeking,
        Fleeing
    }

    public AIState currentState = AIState.Seeking;
    public Transform target;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float fleeDistance = 10f;

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Seeking:
                Seek();
                break;

            case AIState.Fleeing:
                Flee();
                break;
        }
    }

    //seek the player
    void Seek()
    {
        if (target == null) return;

        //move towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        //rotate to face the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //flee from the player
    void Flee()
    {
        if (target == null) return;

        //move away from the target
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        //rotate to face away from the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //switch between seeking and fleeing
    public void SwitchState(AIState newState)
    {
        currentState = newState;
    }
}