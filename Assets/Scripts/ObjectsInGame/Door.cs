using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject[] doors;
    Vector3 offset;

    GameObject BlackOut;

    private void Awake()
    {
        if (gameObject.name == "PierwszeDrzwi")
            EnableDisableRoom(transform, true);
        else
            EnableDisableRoom(transform, false);
    }

    private void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");

        offset = transform.forward * 2;// + new Vector3(1, 0, 0);
        BlackOut = GameObject.Find("BlackOut");
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.tag == "Player")
            {
                Transform przeciwnicy = transform.parent.parent.GetChild(0).Find("Przeciwnicy");
                if(przeciwnicy != null)
                {
                    if (przeciwnicy.childCount != 0)
                        return;
                }
                
                GameObject nearestDoor = SearchForNearestDoor();
                TeleportPlayer(other.gameObject, nearestDoor);
                EnableDisableRoom(nearestDoor.transform, true);
                EnableDisableRoom(transform, false);
                BlackOut.GetComponent<BlackOut>().FadeInFadeOut();
            }
        }
    }

    private GameObject SearchForNearestDoor()
    {
        
        GameObject nearestDoor = null;
        float nearestDist = float.MaxValue;
        foreach (var door in doors)
        {
            if (door != gameObject)
            {
                float dist = Vector3.Distance(door.transform.position, transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestDoor = door;
                }

            }
        }

        return nearestDoor;
    }

    void EnableDisableRoom(Transform door, bool enable)
    {
        Transform doors = door.parent;
        Transform room = doors.parent;
        Transform content = room.GetChild(0);

        content.gameObject.SetActive(enable);
        for(int i=0; i<doors.childCount; i++)
        {
            doors.GetChild(i).GetChild(0).gameObject.SetActive(enable); // Doorframe
            doors.GetChild(i).GetChild(1).gameObject.SetActive(enable); //dfk_door_01
        }

    }

    void TeleportPlayer(GameObject player, GameObject nearestDoor)
    {
        
        Vector3 pos = nearestDoor.transform.position;
        float rotationY = transform.rotation.eulerAngles.y;
        if (rotationY > 85 && rotationY < 95)
            pos += new Vector3(0, 0, 1);
        else if (rotationY > 265 && rotationY < 275)
            pos += new Vector3(0, 0, -1);
        else if (rotationY > 360 || rotationY < 5)
            pos += new Vector3(-1, 0, 0);
        else if (rotationY > 175 && rotationY < 185)
            pos += new Vector3(1, 0, 0);
        player.transform.position = pos;
    }


}
