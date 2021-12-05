using System.Collections.ObjectModel;

namespace MyProcessUsage
{
    internal class VM_MainWindow : ObservableObject
    {
        private M_MainWindow _mainWindow = new M_MainWindow();

        public VM_MainWindow()
        {
            _mainWindow.PropertyChanged += _mainWindow_PropertyChanged;
        }

        public string m_strSearch
        {
            get 
            { 
                return _mainWindow.m_strSearch; 
            }
            set
            {
                _mainWindow.m_strSearch = value;
                NotifyPropertyChanged(nameof(m_strSearch));
            }
        }

        private void _mainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(m_ProcessesStats));
        }

        public ObservableCollection<ProcessStats> m_ProcessesStats
        {
            get
            {
                return _mainWindow.m_ProcessesStats;
            }
        }
    }
}