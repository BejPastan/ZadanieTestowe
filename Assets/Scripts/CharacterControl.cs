using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    CharacterMovement movement;

    public void MoveTo(Vector3 destination)
    {
        movement.MoveTo(destination);
    }

    public void FollowTo(Vector3 destination, float maxRange)
    {
        //find destination point around given point
        Vector3 newDestination = movement.FollowTo(destination, maxRange);
        //Debug.Log("CharacterControl: FollowTo" + newDestination);
        //move to destination point
        movement.MoveTo(newDestination);
    }

    public void CreateStats()
    {
        movement.SetStats();
    }

    //get destination point from movement
    public Vector3 GetDestination()
    {
        return movement.GetDestination();
    }
}
