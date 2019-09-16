using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AlertManager;
using Models.PatientAlert;

namespace MockRepositoryLib
{
    public class MockAlertManager : IAlertManager
    {
        private int m_count;

        public MockAlertManager(ref int count)
        {
            m_count = count;
        }

        public void AlertUsers(PatientAlert alert)
        {
            m_count++;
        }
    }
}
