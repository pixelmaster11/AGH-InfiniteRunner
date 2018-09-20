using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using Enums;
using FSM.Character;
using CharacterSystem.CharacterData;
using CharacterSystem.CharacterComponents;

public class CharManager : MonoBehaviour
{

    public InputType inputMethod;

    //Holds all the playable characters 
    public List<Character> characters;

   
    public int spawnId;

    public CharCamera charCamera;
    
    private Character selectedCharacter;
    private Vector3 spawnPosition = new Vector3(0 , 1.15f, 3f);


    BaseController controller;

 

   



    public void RecenterCharacter(float recenterBy)
    {
        Vector3 recenter = new Vector3(controller.transform.position.x,
                            controller.transform.position.y, controller.transform.position.z - recenterBy);

        controller.transform.position = recenter;
    }





}
