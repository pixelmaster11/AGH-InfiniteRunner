using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;


public class GameManager : MonoBehaviour
{

    private StateMachine fsm = new StateMachine();

    private void Awake()
    {
        fsm.ChangeState(new InitState());
        
    }


    private void Start()
    {
        fsm.ChangeState(new MenuState());
    }


    private void Update()
    {
        fsm.StateUpdate();
    }

}
