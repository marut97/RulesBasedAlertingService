using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PatientVitals;

namespace SharedResources.SharedQueue
{
    public sealed class SharedQueue
    {
        private static SharedQueue instance = null;
        private static readonly object padlock = new object();
        private readonly Mutex mutex = new Mutex();
        private readonly Queue<PatientVitals> vitalsQueue = new Queue<PatientVitals>();
        
        public static SharedQueue Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SharedQueue();
                    }
                    return instance;
                }
            }
        }

        public void Enqueue(PatientVitals vitals)
        {
            mutex.WaitOne();
            vitalsQueue.Enqueue(vitals);
            mutex.ReleaseMutex();
        }

        public PatientVitals Dequeue()
        {
            PatientVitals vitals = new PatientVitals();
            mutex.WaitOne();
            if(vitalsQueue.Any())
                vitals = vitalsQueue.Dequeue();
            mutex.ReleaseMutex();
            return vitals;
        }

        public bool IsEmpty()
        {
            return (vitalsQueue.Count == 0);
        }
    }
}
