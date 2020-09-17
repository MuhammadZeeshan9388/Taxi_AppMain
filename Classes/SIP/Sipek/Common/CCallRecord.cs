namespace Sipek.Common
{
    using System;

    public class CCallRecord
    {
        private int _count;
        private TimeSpan _duration;
        private string _name = "";
        private string _number = "";
        private DateTime _time;
        private ECallType _type;

        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
            }
        }

        public ECallType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

