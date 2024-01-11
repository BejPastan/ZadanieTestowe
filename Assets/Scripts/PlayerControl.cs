using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Detecting mouse click
        if(Input.GetMouseButtonDown(0))
        {
            //check if mouse is not over UI
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                //check if mouse is over map
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.name == "Map")
                    {
                        PlayerClickOnMap();
                    }
                }
            }
        }
        //check if player scroll mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            //check if mouse is over UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("PlayerControl: ScrollOverUI");
                PlayerScrollOverUI();
            }
        }
    }

    private void PlayerClickOnMap()
    {
        ClickOnMap();
    }

    private void PlayerScrollOverUI()
    {
        ScrollOverUI();
    }


    //event handler for player click on map
    public delegate void PlayerClickOnMapEventHandler();
    public static event PlayerClickOnMapEventHandler ClickOnMap;
    //event handler for player scroll mouse wheel
    public delegate void PlayerScrollOverUIEventHandler();
    public static event PlayerScrollOverUIEventHandler ScrollOverUI;
}
