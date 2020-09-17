namespace Sipek.Common
{
    using Sipek.Common.CallControl;
    using System;

    internal class NullFactory : AbstractFactory
    {
        public IStateMachine createStateMachine()
        {
            return new CStateMachine();
        }

        public ITimer createTimer()
        {
            return new NullTimer();
        }
    }
}

