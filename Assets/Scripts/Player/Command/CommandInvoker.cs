using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> moveQueue;

    void Awake()
    {
        moveQueue = new Queue<ICommand>();
    }

    public void EnqueueCommand(ICommand command)
    {
        moveQueue.Enqueue(command);
    }

    public void ExecuteNextCommand()
    {
        if (moveQueue.Count > 0)
        { 
            moveQueue.Dequeue().Execute();
        }
    }
}