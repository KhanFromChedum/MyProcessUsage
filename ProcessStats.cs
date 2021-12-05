using System;
using System.Diagnostics;

namespace MyProcessUsage
{
    public class ProcessStats : ObservableObject
    {
        private PerformanceCounter _perfCpu;
        private PerformanceCounter _perfMemory;

        public void NextValue()
        {
            if (_perfCpu == null)
            {
                _perfCpu = new PerformanceCounter("Process", "% Processor Time", m_strName, true);
                _perfMemory = new PerformanceCounter("Process", "Working Set - Private", m_strName);
            }
            if (!string.IsNullOrEmpty(m_strName))
            {
                try
                {
                    m_fCPUUsage = _perfCpu.NextValue();
                    m_uiMemoryUsage = (uint)_perfMemory.NextValue();
                }
                catch (InvalidOperationException)
                {
                    //deal with exception
                }
            }
        }

        private bool _bSave;

        public bool m_bSave
        {
            get
            {
                return _bSave;
            }
            set
            {
                _bSave = value;
                NotifyPropertyChanged(nameof(m_bSave));
            }
        }

        private string _strName;

        public string m_strName
        {
            get
            {
                return _strName;
            }
            set
            {
                _strName = value;
                NotifyPropertyChanged(nameof(m_strName));
            }
        }

        private uint _uiMemoryUsage;

        public uint m_uiMemoryUsage
        {
            get
            {
                return _uiMemoryUsage;
            }
            set
            {
                _uiMemoryUsage = value;
                NotifyPropertyChanged(nameof(m_uiMemoryUsage));
            }
        }

        private float _fCPUUsage;

        public float m_fCPUUsage
        {
            get
            {
                return _fCPUUsage;
            }
            set
            {
                _fCPUUsage = value;
                NotifyPropertyChanged(nameof(m_fCPUUsage));
            }
        }

        public long m_lLastSaved { get; set; }
    }
}