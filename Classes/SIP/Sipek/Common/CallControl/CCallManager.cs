namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class CCallManager
    {
        private ICallLogInterface _callLog = new NullCallLogger();
        private Dictionary<int, IStateMachine> _calls;
        private IConfiguratorInterface _config = new NullConfigurator();
        private AbstractFactory _factory = new NullFactory();
        private bool _initialized;
        private static CCallManager _instance;
        private IMediaProxyInterface _media = new NullMediaProxy();
        private PendingAction _pendingAction;
        private IVoipProxy _stack = new NullVoipProxy();




        public event DCallStateRefresh CallStateRefresh;



        public event DIncomingCallNotification IncomingCallNotification;

        public void activatePendingAction()
        {
            if (this._pendingAction != null)
            {
                this._pendingAction.Activate();
            }
            this._pendingAction = null;
        }

        public IStateMachine createOutboundCall(string number)
        {
            int defaultAccountIndex = this.Config.DefaultAccountIndex;
            return this.createOutboundCall(number, defaultAccountIndex);
        }

        public IStateMachine createOutboundCall(string number, int accountId)
        {
            if (this.IsInitialized)
            {
                if (this.getNoCallsInStates(6) > 0)
                {
                    return new NullStateMachine();
                }
                if (this.getNoCallsInState(EStateId.ACTIVE) == 0)
                {
                    IStateMachine machine = this.Factory.createStateMachine();
                    if (machine == null)
                    {
                        return null;
                    }
                    int key = machine.State.makeCall(number, accountId);
                    if (key == -1)
                    {
                        return new NullStateMachine();
                    }
                    try
                    {
                        machine.Session = key;
                        this._calls.Add(key, machine);
                    }
                    catch (ArgumentException)
                    {
                        this._calls[key].destroy();
                        this._calls.Add(key, machine);
                    }
                    return machine;
                }
                this._pendingAction = new PendingAction(EPendingActions.ECreateSession, number, accountId);
                this.getCallInState(EStateId.ACTIVE).State.holdCall();
            }
            return new NullStateMachine();
        }

        internal void destroySession(int session)
        {
            bool flag = true;
            if (this.getCall(session).DisableStateNotifications)
            {
                flag = false;
            }
            this._calls.Remove(session);
            if (flag)
            {
                this.updateGui(session);
            }
        }

        public ICollection<IStateMachine> enumCallsInState(EStateId stateId)
        {
            List<IStateMachine> list = new List<IStateMachine>();
            foreach (KeyValuePair<int, IStateMachine> pair in this._calls)
            {
                if (stateId == pair.Value.State.Id)
                {
                    list.Add(pair.Value);
                }
            }
            return list;
        }

        public IStateMachine getCall(int session)
        {
            if ((this._calls.Count != 0) && this._calls.ContainsKey(session))
            {
                return this._calls[session];
            }
            return new NullStateMachine();
        }

        public IStateMachine getCallInState(EStateId stateId)
        {
            if (this._calls.Count != 0)
            {
                foreach (KeyValuePair<int, IStateMachine> pair in this._calls)
                {
                    if (pair.Value.State.Id == stateId)
                    {
                        return pair.Value;
                    }
                }
            }
            return new NullStateMachine();
        }

        public int getNoCallsInState(EStateId stateId)
        {
            int num = 0;
            foreach (KeyValuePair<int, IStateMachine> pair in this._calls)
            {
                if (stateId == pair.Value.State.Id)
                {
                    num++;
                }
            }
            return num;
        }

        private int getNoCallsInStates(int states)
        {
            int num = 0;
            foreach (KeyValuePair<int, IStateMachine> pair in this._calls)
            {
                if ((states & Convert.ToInt32(pair.Value.State.Id)) ==states)
                {
                    num++;
                }
            }
            return num;
        }

        public int Initialize()
        {
            return this.Initialize(this._stack);
        }

        public int Initialize(IVoipProxy proxy)
        {
            this._stack = proxy;
            int num = 0;
            if (!this.IsInitialized)
            {
                ICallProxyInterface.CallStateChanged += new DCallStateChanged(this.OnCallStateChanged);
                ICallProxyInterface.CallIncoming += new DCallIncoming(this.OnIncomingCall);
                ICallProxyInterface.CallNotification += new DCallNotification(this.OnCallNotification);

              //  this.AnswerCallNotification += new DAnswerCallNotification(CCallManager_AnswerCallNotification);

                this._calls = new Dictionary<int, IStateMachine>();
            }
            num = this.StackProxy.initialize();
            if (num == 0)
            {
                this._initialized = true;
            }
            else if(num==420005)
            {
                throw new Exception("Audio Device not working correctly");
            }
            return num;
        }

      



        //void CCallManager_AnswerCallNotification(int sessionId, string number, string info)
        //{

           

        //       CallAnswerNotification(sessionId, number, info);
        //}



        private void OnCallNotification(int callId, ECallNotification notFlag, string text)
        {
            if (notFlag == ECallNotification.CN_HOLDCONFIRM)
            {
                IStateMachine machine = this.getCall(callId);
                if (!machine.IsNull)
                {
                    machine.State.onHoldConfirm();
                }
            }
        }

        private void OnCallReplaced(int oldid, int newid)
        {
            IStateMachine machine = this.CallList[oldid];
            this._calls.Remove(oldid);
            machine.Session = newid;
            this.CallList.Add(newid, machine);
        }

        private void OnCallStateChanged(int callId, ESessionState callState, string info)
        {
            if (callState == ESessionState.SESSION_STATE_INCOMING)
            {
                IStateMachine machine = this.Factory.createStateMachine();
                if (machine.IsNull && this.Config.CFBFlag)
                {
                    this.StackProxy.createCallProxy().serviceRequest(5, this.Config.CFBNumber);
                }
                else
                {
                    machine.Session = callId;
                    if (this.CallList.ContainsKey(callId))
                    {
                        this.CallList[callId].State.endCall();
                    }
                    else
                    {
                        this._calls.Add(callId, machine);
                    }
                }
            }
            else
            {
           


                IStateMachine machine2 = this.getCall(callId);
                if (!machine2.IsNull)
                {
                    switch (callState)
                    {
                        

                        case ESessionState.SESSION_STATE_CALLING:
                        case ESessionState.SESSION_STATE_INCOMING:
                        case ESessionState.SESSION_STATE_CONFIRMED:
                           return;

                        case ESessionState.SESSION_STATE_EARLY:
                            machine2.State.onAlerting();
                            return;

                        case ESessionState.SESSION_STATE_CONNECTING:
                            machine2.State.onConnect();
                            return;

                        case ESessionState.SESSION_STATE_DISCONNECTED:
                            OnIncomingCall(0, machine2.CallingNumber, "Answer");                              
                            machine2.State.onReleased();                          

                            return;
                    }
                }


                

            }
        }

        private void OnIncomingCall(int sessionId, string number, string info)
        {

            if (info == "Answer")
            {
                if (this.IncomingCallNotification != null)
                {
                    this.IncomingCallNotification(sessionId, number, info);
                }

            }
            else
            {

                IStateMachine machine = this.getCall(sessionId);

                if (!machine.IsNull)
                {
                    machine.State.incomingCall(number, info);
                    if ((this.IncomingCallNotification != null) && !machine.DisableStateNotifications)
                    {
                        this.IncomingCallNotification(sessionId, number, info);
                    }
                }
            }
        }

        public void onUserAnswer(int session)
        {
            List<IStateMachine> list = (List<IStateMachine>) this.enumCallsInState(EStateId.ACTIVE);
            if (list.Count > 0)
            {
                IStateMachine machine = list[0];
                if (!machine.IsNull)
                {
                    machine.State.holdCall();
                }
                this._pendingAction = new PendingAction(EPendingActions.EUserAnswer, session);
            }
            else
            {
                this[session].State.acceptCall();
            }
        }

        public void onUserConference(int session)
        {
            if ((this.getNoCallsInState(EStateId.ACTIVE) == 1) && (this.getNoCallsInState(EStateId.HOLDING) >= 1))
            {
                IStateMachine machine = this.getCallInState(EStateId.HOLDING);
                machine.State.retrieveCall();
                machine.State.conferenceCall();
            }
        }

        public void onUserDialDigit(int session, string digits, EDtmfMode mode)
        {
            this[session].State.dialDtmf(digits, mode);
        }

        public void onUserHoldRetrieve(int session)
        {
            IAbstractState state = this[session].State;
            if (state.Id == EStateId.ACTIVE)
            {
                this.getCall(session).State.holdCall();
            }
            else if (state.Id == EStateId.HOLDING)
            {
                if (this.getNoCallsInState(EStateId.ACTIVE) > 0)
                {
                    IStateMachine machine = ((List<IStateMachine>) this.enumCallsInState(EStateId.ACTIVE))[0];
                    if (!machine.IsNull)
                    {
                        machine.State.holdCall();
                    }
                    this._pendingAction = new PendingAction(EPendingActions.EUserHold, session);
                }
                else
                {
                    this[session].State.retrieveCall();
                }
            }
        }

        public void onUserRelease(int session)
        {
            this[session].State.endCall();
        }

        public void onUserTransfer(int session, string number)
        {
            this[session].State.xferCall(number);
        }

        public void Shutdown()
        {
            IStateMachine[] array = new IStateMachine[this.CallList.Count];
            this.CallList.Values.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                array[i].destroy();
            }
            this.CallList.Clear();
            this.StackProxy.shutdown();
            this._initialized = false;
            this.CallStateRefresh = null;
            this.IncomingCallNotification = null;
            ICallProxyInterface.CallStateChanged -= new DCallStateChanged(this.OnCallStateChanged);
            ICallProxyInterface.CallIncoming -= new DCallIncoming(this.OnIncomingCall);
            ICallProxyInterface.CallNotification -= new DCallNotification(this.OnCallNotification);
            this.StackProxy.CallReplaced -= new DCallReplaced(this.OnCallReplaced);
        }

        public void updateGui(int sessionId)
        {
            if (this.CallStateRefresh != null)
            {
                this.CallStateRefresh(sessionId);
            }
        }

        public Dictionary<int, IStateMachine> CallList
        {
            get
            {
                return this._calls;
            }
        }

        public ICallLogInterface CallLogger
        {
            get
            {
                return this._callLog;
            }
            set
            {
                this._callLog = value;
            }
        }

        public IConfiguratorInterface Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }

        public int Count
        {
            get
            {
                return this._calls.Count;
            }
        }

        public AbstractFactory Factory
        {
            get
            {
                return this._factory;
            }
            set
            {
                this._factory = value;
            }
        }

        public static CCallManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CCallManager();
                }
                return _instance;
            }
        }

        public bool Is3Pty
        {
            get
            {
                if (this.getNoCallsInState(EStateId.ACTIVE) != 2)
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this._initialized;
            }
        }

        public IStateMachine this[int index]
        {
            get
            {
                if (!this._calls.ContainsKey(index))
                {
                    return new NullStateMachine();
                }
                return this._calls[index];
            }
        }

        public IMediaProxyInterface MediaProxy
        {
            get
            {
                return this._media;
            }
            set
            {
                this._media = value;
            }
        }

        public IVoipProxy StackProxy
        {
            get
            {
                return this._stack;
            }
            set
            {
                this._stack = value;
            }
        }

        private enum EPendingActions
        {
            EUserAnswer,
            ECreateSession,
            EUserHold
        }

        private class PendingAction
        {
            private int _accountId;
            private CCallManager.EPendingActions _actionType;
            private string _number;
            private int _sessionId;

            public PendingAction(CCallManager.EPendingActions action, int sessionId)
            {
                this._actionType = action;
                this._sessionId = sessionId;
            }

            public PendingAction(CCallManager.EPendingActions action, string number, int accId)
            {
                this._actionType = action;
                this._sessionId = -1;
                this._number = number;
                this._accountId = accId;
            }

            public void Activate()
            {
                switch (this._actionType)
                {
                    case CCallManager.EPendingActions.EUserAnswer:
                        CCallManager.Instance.onUserAnswer(this._sessionId);
                        return;

                    case CCallManager.EPendingActions.ECreateSession:
                        CCallManager.Instance.createOutboundCall(this._number, this._accountId);
                        return;

                    case CCallManager.EPendingActions.EUserHold:
                        CCallManager.Instance.onUserHoldRetrieve(this._sessionId);
                        return;
                }
            }

            private delegate void DPendingAnswer(int sessionId);

            private delegate void DPendingCreateSession(string number, int accountId);
        }
    }
}

