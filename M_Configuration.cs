namespace MyProcessUsage
{
    internal class M_Configuration: ObservableObject
    {
        public static uint m_uiSaveIntervall { get; set; } = 60;
        public static string m_strSavePath { get; set; } = @"c:\temp";
    }
}