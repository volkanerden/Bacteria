using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControl : MonoBehaviour
{
    
    private IPlayerState currentState;

    [SerializeField] private Move move;
    [SerializeField] private Fire fire;

    private void Awake()
    {
        move = GetComponent<Move>();
        fire = GetComponent<Fire>();

        currentState = new IdleState(move, fire);
    }
    private void Update()
    {
        currentState.UpdateState();
       
        // Move State
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentState.SetMoveEnabled(true);
            currentState.SetFireEnabled(false);
            move.EnqueueMoveCommand(target);
            currentState = new MovingState(move, fire);
        }

        // Fire State
        if (!move.isMoving)
        {
            currentState.SetFireEnabled(true);
            currentState = new FiringState(move, fire);
        }

        // Idle State
        if (move.isMoving)
        {
            currentState.SetFireEnabled(false);
            currentState.SetMoveEnabled(true);
            currentState = new IdleState(move, fire);
        }
    }
}