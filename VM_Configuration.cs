using System.Windows.Forms;
using System.Windows.Input;

namespace MyProcessUsage
{
    internal class VM_Configuration : ObservableObject
    {

        public VM_Configuration()
        {
            m_uiSaveIntervall = M_Configuration.m_uiSaveIntervall;
            m_strSavePath = M_Configuration.m_strSavePath;
        }
        public uint m_uiSaveIntervall
        {
            get; set;
        }


        private string _strSavePath;
        public string m_strSavePath 
        { 
            get
            {
                return _strSavePath;
            }
            set
            {
                _strSavePath = value;
                NotifyPropertyChanged(nameof(m_strSavePath));
            }
        }

        private ICommand _cmdFolderSelection;

        public ICommand m_cmdFolderSelection
        {
            get
            {
                if (_cmdFolderSelection == null)
                {
                    _cmdFolderSelection = new RelayCommand((o) =>
                    {
                        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            m_strSavePath = folderBrowserDialog1.SelectedPath;
                        }
                    });
                }
                return _cmdFolderSelection;
            }
        }

        private ICommand _cmdOk;

        public ICommand m_cmdOk
        {
            get
            {
                if (_cmdOk == null)
                {
                    _cmdOk = new RelayCommand((o) =>
                    {
                        IClosable closableWindow = o as IClosable;
                        closableWindow.Close();
                        M_Configuration.m_uiSaveIntervall = m_uiSaveIntervall;
                        M_Configuration.m_strSavePath = m_strSavePath;
                    });
                }
                return _cmdOk;
            }
        }

        private ICommand _cmdCancel;

        public ICommand m_cmdCancel
        {
            get
            {
                if (_cmdCancel == null)
                {
                    _cmdCancel = new RelayCommand((o) =>
                    {
                        IClosable closableWindow = o as IClosable;
                        closableWindow.Close();
                    });
                }
                return _cmdCancel;
            }
        }
    }
}