using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamControl : MonoBehaviour
{
    int LeaderNum = 1;


    //subscribe to event
    private void OnEnable()
    {
        PlayerControl.ClickOnMap += MoveToDestination;
    }

    public void MoveToDestination()
    {
        Debug.Log("TeamControl: MoveToDestination");
        //check if leader is selected
        if (LeaderNum >= 0)
        {
            GetDestinationPoint();
            Debug.Log("TeamControl: Leader selected");
        }
        else
        {
            Debug.Log("TeamControl: No leader selected");
            return;
        }
    }

    private void GetDestinationPoint()
    {
        //raycast to get destination point
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 destination = hit.point;


        }
    }
}
