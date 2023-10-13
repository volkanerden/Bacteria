using System;

public interface IPlayerState
{
   // public event Action<IPlayerState> OnPlayerStateChanged;
    void UpdateState();
    void SetMoveEnabled(bool enabled);
    void SetFireEnabled(bool enabled);
}

public class IdleState : IPlayerState
{
    private readonly Move move;
    private readonly Fire fire;

    //public event Action<IPlayerState> OnPlayerStateChanged;

    public IdleState(Move move, Fire fire)
    {
        this.move = move;
        this.fire = fire;
    }

    public void UpdateState()
    {
    }
    public void SetMoveEnabled(bool enabled)
    {
        move.enabled = enabled;
    }
    public void SetFireEnabled(bool enabled)
    {
        fire.enabled = enabled;
    }
}

public class MovingState : IPlayerState
{
    private readonly Move move;
    private readonly Fire fire;

    public MovingState(Move move, Fire fire)
    {
        this.move = move;
        this.fire = fire;
    }

    public void UpdateState()
    {
        if (move.HasReachedTargetPosition())
        {
            move.StopMoving();
            SetMoveEnabled(false);
            SetFireEnabled(true);
        }
    }
    public void SetMoveEnabled(bool enabled)
    {
        move.enabled = enabled;  
    }
    public void SetFireEnabled(bool enabled)
    {
        fire.enabled = enabled;
    }
}

public class FiringState : IPlayerState
{
    private readonly Move move;
    private readonly Fire fire;

    public FiringState(Move move, Fire fire)
    {
        this.move = move;
        this.fire = fire;
    }

    public void UpdateState()
    {
        fire.StartFiring();
    }
    public void SetMoveEnabled(bool enabled)
    {
        move.enabled = enabled;
    }
    public void SetFireEnabled(bool enabled)
    {
        fire.enabled = enabled;
    }
}
