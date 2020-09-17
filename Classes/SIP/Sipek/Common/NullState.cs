namespace Sipek.Common
{
    using System;

    internal class NullState : IAbstractState
    {
        public NullState() : base(new NullStateMachine())
        {
        }

        public override bool conferenceCall()
        {
            return false;
        }

        public override void onEntry()
        {
        }

        public override void onExit()
        {
        }
    }
}

