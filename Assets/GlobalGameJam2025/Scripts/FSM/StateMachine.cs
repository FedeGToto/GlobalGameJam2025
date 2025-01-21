public class StateMachine<T>
{
    public State<T> CurrentState { get; private set; }
    public T Owner;

    public StateMachine(T owner)
    {
        this.Owner = owner;
        CurrentState = null;
    }

    public void ChangeState(State<T> newState)
    {
        if (CurrentState != null)
            CurrentState.Exit(Owner);
        CurrentState = newState;
        CurrentState.Enter(Owner);
    }

    public void Update()
    {
        if (CurrentState != null)
            CurrentState.Update(Owner);
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.FixedUpdate(Owner);
    }
}
