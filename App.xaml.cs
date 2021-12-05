using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

/*

 */

namespace MyProcessUsage
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static async Task<string[]> GetAllInstances()
        {
            string[] instancesName = await Task<string[]>.Run(()=> new PerformanceCounterCategory("Process").GetInstanceNames());

            return instancesName;
        }

        public static ProcessStats ProcessConverter(string instanceName)
        {
            ProcessStats processStats = new ProcessStats();
            processStats.m_strName = instanceName;
            return processStats;
        }

        public static void Save(ProcessStats processStats)
        {
            long lTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - processStats.m_lLastSaved;
            if (processStats.m_bSave)
            {
                if (lTime > (M_Configuration.m_uiSaveIntervall*1000))
                {
                    processStats.m_lLastSaved = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    if (!File.Exists("c:\\temp\\" + processStats.m_strName.Replace("#", "_") + ".csv"))
                    {
                        StreamWriter sw = File.CreateText("c:\\temp\\" + processStats.m_strName.Replace("#", "_") + ".csv");
                        sw.WriteLine("Date;Memory;CPU");
                        sw.Close();
                    }
                    else
                    {
                        File.AppendAllText("c:\\temp\\" + processStats.m_strName.Replace("#", "_") + ".csv", DateTime.Now.ToString() + ";" + processStats.m_uiMemoryUsage.ToString() + ";" + processStats.m_fCPUUsage.ToString() + Environment.NewLine);
                    }
                }
            }
        }

        public static async Task<ObservableCollection<ProcessStats>> ProcessStatsFactory()
        {
            ObservableCollection<ProcessStats> obsProcessesStats = new ObservableCollection<ProcessStats>();
            string[] instances = await GetAllInstances();
            foreach (var instance in instances)
            {
                obsProcessesStats.Add(ProcessConverter(instance));
            }

            return obsProcessesStats;
        }
    }
}