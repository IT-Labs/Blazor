using System.ComponentModel;

namespace Core.Shared.Enums
{
    public enum Permissions 
    {
        [Description("Print Batch Cover Page")]
        PrintBatchCoverPage,
        [Description("Print Barcode Stikers")]
        PrintBarcodeStikers,
        [Description("Scanning")]
        Scanning,
        [Description("Recognition Application")]
        RecognitionApplication,
        [Description("Data Entry Application")]
        DataEntryApplication
    }
}