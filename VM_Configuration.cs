using System.Windows.Input;

namespace MyProcessUsage
{
    internal class VM_Configuration : ObservableObject
    {

        public VM_Configuration()
        {
            m_uiSaveIntervall = M_Configuration.m_uiSaveIntervall;
        }
        public uint m_uiSaveIntervall
        {
            get; set;
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