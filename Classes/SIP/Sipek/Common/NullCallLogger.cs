namespace Sipek.Common
{
    using System;
    using System.Collections.Generic;

    public class NullCallLogger : ICallLogInterface
    {
        public void addCall(ECallType type, string number, string name, DateTime time, TimeSpan duration)
        {
        }

        public void deleteRecord(CCallRecord record)
        {
        }

        public Stack<CCallRecord> getList()
        {
            return null;
        }

        public Stack<CCallRecord> getList(ECallType type)
        {
            return null;
        }

        public void save()
        {
        }
    }
}

