using System.Collections.ObjectModel;
using System.Windows.Input;

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

        private ICommand _cmdConfig;
        public ICommand m_cmdConfig
        {
            get
            {
                if (_cmdConfig == null)
                {
                    _cmdConfig = new RelayCommand((o) =>
                    {
                        Configuration configurationWindow = new Configuration();
                        configurationWindow.ShowDialog();
                    });
                }
                return _cmdConfig;
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