using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;

namespace MyProcessUsage
{
    internal class M_MainWindow : ObservableObject
    {
        public M_MainWindow()
        {
            aTimer.Elapsed += new ElapsedEventHandler(ATimer_Elapsed);
            aTimer.AutoReset = true;
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        public string m_strSearch { get; set; }

        private bool _bIsInit = false;

        private ObservableCollection<ProcessStats> _processesStats;

        public ObservableCollection<ProcessStats> m_ProcessesStats
        {
            get
            {
                return _processesStats;
            }
            set
            {
                _processesStats = value;
                NotifyPropertyChanged();
            }
        }

        private async void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_bIsInit)
            {
                _bIsInit = true;
                m_ProcessesStats = await App.ProcessStatsFactory();
            }
            if (m_ProcessesStats != null)
            {
                List<string> instances =  new List<string>(await App.GetAllInstances());
                if(!string.IsNullOrEmpty(m_strSearch))
                {
                    instances = new List<string>(instances.Where(x => x.ToLower().Contains(m_strSearch.ToLower())));
                }

                //var deletedProcesses =new List<ProcessStats>( m_ProcessesStats.Where(process => !instances.Any( )));
                //var AddedProcess = new List<string>(instances.Where(process => !m_ProcessesStats.Any(  )));
                //App.Current?.Dispatcher.Invoke( () => {

                //    foreach(var item in deletedProcesses)
                //    {
                //        m_ProcessesStats.Remove(item);
                //    }

                //    foreach (var item in AddedProcess)
                //    {
                //        m_ProcessesStats.Add(App.ProcessConverter(item));
                //    }
                //});
                Parallel.ForEach(m_ProcessesStats, (processStats) => {  processStats.NextValue(); });



            }
       
        }

        private static System.Timers.Timer aTimer = new System.Timers.Timer();
    }
}