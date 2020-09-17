namespace Sipek.Common
{
    public interface AbstractFactory
    {
        IStateMachine createStateMachine();
        ITimer createTimer();
    }
}

