using System.Collections.ObjectModel;
using System.Diagnostics;
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
            string[] instancesName = new PerformanceCounterCategory("Process").GetInstanceNames();
            return instancesName;


        }

        public static  ProcessStats ProcessConverter(string instanceName)
        {
            ProcessStats processStats = new ProcessStats();
            processStats.m_strName = instanceName;
            return processStats;
        }

        public static async Task<ObservableCollection<ProcessStats>> ProcessStatsFactory()
        {
            ObservableCollection<ProcessStats> obsProcessesStats = new ObservableCollection<ProcessStats>();
            string[] instances = await GetAllInstances();
            foreach (var instance in instances)
            {
                obsProcessesStats.Add( ProcessConverter(instance));
            }

            return obsProcessesStats;
        }
    }
}