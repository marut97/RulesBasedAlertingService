using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PatientAlert;

namespace SharedResources.AlertMonitorList
{
    public delegate void AlertMonitoringFunction(PatientAlert alert);

    public sealed class AlertMonitorList 
    {
        private static AlertMonitorList instance = null;
        private static readonly object Padlock = new object();
        private readonly Mutex mutex;
        private readonly Dictionary<string, AlertMonitoringFunction> alertMonitoringList;

        private AlertMonitorList()
        {
            mutex = new Mutex();
            alertMonitoringList = new Dictionary<string, AlertMonitoringFunction>();
        }

        public static AlertMonitorList Instance
        {
            get
            {
                lock (Padlock)
                {
                    return instance ?? (instance = new AlertMonitorList());
                }
            }
        }

        public void Subscribe(string id, AlertMonitoringFunction function)
        {
            mutex.WaitOne();
            if(!alertMonitoringList.ContainsKey(id))
                alertMonitoringList.Add(id, new AlertMonitoringFunction(function));
            mutex.ReleaseMutex();
        }

        public void Unsubscribe(string id)
        {
            mutex.WaitOne();
            if (alertMonitoringList.ContainsKey(id))
                alertMonitoringList.Remove(id);
            mutex.ReleaseMutex();
        }

        public bool IsEmpty()
        {
            return (!alertMonitoringList.Any());
        }

        public AlertMonitoringFunction TryGetValue(string id)
        {
            if (alertMonitoringList.ContainsKey(id))
            {
                AlertMonitoringFunction func;
                alertMonitoringList.TryGetValue(id, out func);
                return func;
            }
            return null;
        }

        public void Dispose()
        {
            mutex?.Dispose();
        }
    }
}
