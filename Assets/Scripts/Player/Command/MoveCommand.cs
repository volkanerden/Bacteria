using UnityEngine;

public class MoveCommand : ICommand
{
    private Move moveComponent;
    private Vector2 target;

    public MoveCommand(Move moveComponent, Vector2 target)
    {
        this.moveComponent = moveComponent;
        this.target = target;
    }

    public void Execute()
    {
        moveComponent.MoveTo(target);
    }
}