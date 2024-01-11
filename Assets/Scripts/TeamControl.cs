using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamControl : MonoBehaviour
{
    [SerializeField]
    float maxRange = 10;
    int LeaderNum = -1;
    public CharacterControl[] characters;
    [SerializeField]
    UIManager UIManager;

    //subscribe to event  
    private void OnEnable()
    {
        PlayerControl.ClickOnMap += MoveToDestination;
    }

    public void FindCharacters()
    {
        //find all characters in scene
        characters = FindObjectsOfType<CharacterControl>();
        Debug.Log("TeamControl: FindCharacters"+characters.Length);
        //set stats for each character
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].CreateStats();
        }
    }

    public void MoveToDestination()
    {
        //Debug.Log("TeamControl: MoveToDestination");
        //check if leader is selected
        if (LeaderNum >= 0)
        {
            Vector3 destination = GetDestinationPoint();
            //Debug.Log("TeamControl: Leader selected");
            //moveing each character to destination point
            for(int i = 0; i < characters.Length; i++)
            {
                if(i == LeaderNum)
                {
                    Debug.Log("TeamControl: Leader");
                    //move leader to destination point
                    characters[i].MoveTo(destination);
                }
                else
                {
                    //follow leader
                    Debug.Log("TeamControl: Follower");
                    characters[i].FollowTo(destination, maxRange);
                }
            }
        }
        else
        {
            //show message in info window if leader is not selected
            UIManager.ShowInfo("Leader is not selected");
        }
    }

    private Vector3 GetDestinationPoint()
    {
        //raycast to get destination point
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
    }

    //selecting leader
    public void SetLeader(int num)
    {
        Debug.Log("TeamControl: SetLeader " + num);
        LeaderNum = num;
    }
}
