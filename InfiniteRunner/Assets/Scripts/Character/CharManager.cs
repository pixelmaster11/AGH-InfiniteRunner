﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using FSM;
using FSM.Character;

public class CharManager : MonoBehaviour
{

    public InputMethod inputMethod;

    //Holds all the playable characters 
    public List<Character> characters;

    public State[] characterStates;


    public int spawnId;

    public CharCamera charCamera;
    
    private Character selectedCharacter;
    private Vector3 spawnPosition = new Vector3(0 , 0.7f, -3.64f);

    CharController controller;
  



    public void SpawnCharacter(int charID)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        selectedCharacter = characters[charID];
        selectedCharacter.transform.position = spawnPosition;
        selectedCharacter.transform.rotation = Quaternion.identity;
        selectedCharacter.gameObject.SetActive(true);

        controller = selectedCharacter.GetComponent<CharController>();
        controller.enabled = true;
        controller.SetMovementData(selectedCharacter.characterMovementData);
  
        charCamera.SetLookAt(selectedCharacter.transform);
    }



    private void Start()
    {
        SpawnCharacter(spawnId);
        CharacterInput.SetInputMethod(inputMethod);
        
    }

    private void Update()
    {
        CharacterInput.ResetInputs();
        CharacterInput.CollectInputs();
    
    }

}
