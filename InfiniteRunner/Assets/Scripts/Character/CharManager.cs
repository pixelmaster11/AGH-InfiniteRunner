using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using FSM;
using FSM.Character;

public class CharManager : MonoBehaviour
{

    public InputMethod inputMethod;

    [SerializeField]
    CharMovementData characterMoveData;

    [SerializeField]
    CharController controller;

    private void Start()
    {
        CharacterInput.SetInputMethod(inputMethod);
        controller.SetMovementData(characterMoveData);
    }

    private void Update()
    {
        CharacterInput.ResetInputs();
        CharacterInput.CollectInputs();
    
    }

}
