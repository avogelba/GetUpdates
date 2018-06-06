using System;
//using WUApiInterop;
using WUApiLib;

namespace GetUpdates
{

    /// <summary>
    /// Class for DataGrid
    /// </summary>
    public class Update
    {
        public int ID { get; set; }
        public string KB { get; set; }
        public DateTime Date { get; set; }
        //public string KB { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SupportUrl { get; set; }
        public int HResult { get; set; }
        public string ServiceID { get; set; }
        public string UninstallationNotes { get; set; }
        //public int UnmappedResultCode { get; set; }
        public string ClientApplicationID { get; set; }
        //WUA Specials
        //public UpdateOperation Operation { get; set; }
        public OperationResultCode ResultCode { get; set; }
        //public ServerSelection serverSelection { get; set; }
        //public StringCollection UninstallationSteps { get; set; } //https://msdn.microsoft.com/en-us/library/windows/desktop/aa386083(v=vs.85).aspx
        //public IUpdateIdentity UpdateIdentity { get; set; }
        
    }
}
