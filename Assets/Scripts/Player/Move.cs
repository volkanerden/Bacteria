using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 target;
    private Vector2 direction;
    public bool isMoving = false;
    private CommandInvoker commandInvoker;

    void Start()
    {
        target = transform.position;
        commandInvoker = new CommandInvoker();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnqueueMoveCommand(target);
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.right = direction;
        }

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            isMoving = false;
            commandInvoker.ExecuteNextCommand();
        }
    }

    public void EnqueueMoveCommand(Vector2 targetPosition)
    {
        MoveCommand moveCommand = new MoveCommand(this, targetPosition);
        commandInvoker.EnqueueCommand(moveCommand);

        if(HasReachedTargetPosition())
        {
            commandInvoker.ExecuteNextCommand();
        }
    }

    public void MoveTo(Vector2 target)
    {
        this.target = target;
        direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        isMoving = true;
    }

    public bool HasReachedTargetPosition()
    {
        return !isMoving;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
