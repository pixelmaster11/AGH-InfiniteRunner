using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 0. Init
    /// 1. Menu
    /// 2. GamePlay
    /// 3. GameOver
    /// 4. Pause
    /// </summary>
    [SerializeField]
    State[] gameStates;
    

    private StateMachine fsm = new StateMachine();

    private void Awake()
    {
        fsm.ChangeState(gameStates[0]);
        
    }


    private void Start()
    {
        fsm.ChangeState(gameStates[1]);
    }


    private void Update()
    {
        fsm.StateUpdate();
    }

}
