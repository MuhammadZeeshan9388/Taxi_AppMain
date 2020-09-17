namespace Sipek.Common
{
    using System;
    using System.Collections.Generic;

    public interface ICallLogInterface
    {
        void addCall(ECallType type, string number, string name, DateTime time, TimeSpan duration);
        void deleteRecord(CCallRecord record);
        Stack<CCallRecord> getList();
        Stack<CCallRecord> getList(ECallType type);
        void save();
    }
}

