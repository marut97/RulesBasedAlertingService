using System;
using System.Collections.Generic;
using System.IO;
using Contracts.VitalsRepository;
using Models.PatientVitals;

namespace DataAccessLayer.VitalsRepositoryLib
{
    public class VitalsRepository : IVitalsRepository
    {
        private readonly string vitalsRepositoryPath;
        public VitalsRepository(bool test = false)
        {
            //Should provide relative path in future
            vitalsRepositoryPath = !test ? "C:\\VitalsDB\\ProductionDatabase" : "C:\\VitalsDB\\TestDatabase";
        }
        public List<PatientVitals> ReadAllVitals(string patientId)
        {
            var path = Path.Combine(vitalsRepositoryPath, patientId + ".txt");
            string[] lines = new string[] { };
            if(File.Exists(path))
                lines = File.ReadAllLines(path);
            List<PatientVitals> vitals = new List<PatientVitals>();
            foreach (string temp in lines)
                vitals.Add(StringToVitals(temp));
            return vitals;
        }

        private PatientVitals StringToVitals(string input)
        {
            string[] inputArray = input.Split('/');
            PatientVitals vitals = new PatientVitals
            {
                PatientId = inputArray[0],
                Vitals = new List<Vitals>()
            };
            for (var i = 2; i < inputArray.Length - 1; i++)
            {
                Vitals info = new Vitals { DeviceId = inputArray[i] };
                i++;

                info.Value = Convert.ToDouble(inputArray[i]);
                vitals.Vitals.Add(info);
            }
            return vitals;
        }

        public void WriteVitals(PatientVitals vitals)
        {
            var path = Path.Combine(vitalsRepositoryPath, vitals.PatientId + ".txt");
            var file = File.AppendText(path);
            file.WriteLine(vitals.ToString());
            file.Close();
        }
    }
}
