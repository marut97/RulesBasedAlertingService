using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PatientVitals;

namespace SharedResources.VitalsMonitorList
{
    public delegate void VitalsMonitoringFunction(PatientVitals vitals);

    public sealed class VitalsMonitorList
    {
        private static VitalsMonitorList instance = null;
        private static readonly object Padlock = new object();
        private readonly Mutex mutex;
        private readonly Dictionary<string, Dictionary<string, VitalsMonitoringFunction>> vitalsMonitoringList;

        VitalsMonitorList()
        {
            mutex = new Mutex();
            vitalsMonitoringList = new Dictionary<string, Dictionary<string, VitalsMonitoringFunction>>();
        }

        public static VitalsMonitorList Instance
        {
            get
            {
                lock (Padlock)
                {
                    return instance ?? (instance = new VitalsMonitorList());
                }
            }
        }

        public void Subscribe(string patientId, string id, VitalsMonitoringFunction function)
        {
            mutex.WaitOne();
            Dictionary<string, VitalsMonitoringFunction> subscribers;
            vitalsMonitoringList.TryGetValue(patientId, out subscribers);
            if (subscribers != null)
                subscribers.Add(patientId, function);
            else
            {
                subscribers = new Dictionary<string, VitalsMonitoringFunction>();
                subscribers.Add(id, function);
                vitalsMonitoringList.Add(patientId, subscribers);
            }
            mutex.ReleaseMutex();
        }

        public void Unsubscribe(string patientId, string id)
        {
            mutex.WaitOne();
            Dictionary<string, VitalsMonitoringFunction> subscribers;
            vitalsMonitoringList.TryGetValue(patientId, out subscribers);
            if (subscribers != null)
            {
                subscribers.Remove(id);
                if (!subscribers.Any())
                    vitalsMonitoringList.Remove(patientId);
            }

            mutex.ReleaseMutex();
        }

        public bool IsEmpty()
        {
            return (!vitalsMonitoringList.Any());
        }

        public List<VitalsMonitoringFunction> TryGetValue(string id)
        {
            List<VitalsMonitoringFunction> subscriberList = new List<VitalsMonitoringFunction>();
            if (vitalsMonitoringList.ContainsKey(id))
            {
                Dictionary<string, VitalsMonitoringFunction> subList;
                vitalsMonitoringList.TryGetValue(id, out subList);
                foreach (var sub in subList)
                {
                    subscriberList.Add(sub.Value);
                }

                return subscriberList;
            }
            return new List<VitalsMonitoringFunction>();
        }
    }
}
