using System.Runtime.Serialization;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;

[Serializable]
public sealed class GenerateSprintReportException : Exception
{
    public GenerateSprintReportErrorCode ErrorCode { get; init; }

    public GenerateSprintReportException() : this(GenerateSprintReportErrorCode.Unknown) { }

    public GenerateSprintReportException(string message) : this(GenerateSprintReportErrorCode.Unknown, message) { }

    public GenerateSprintReportException(string message, Exception innerException) : this(GenerateSprintReportErrorCode.Unknown, message, innerException) { }

    public GenerateSprintReportException(GenerateSprintReportErrorCode errorCode) : base($"Error code: {errorCode}")
    {
        ErrorCode = errorCode;
    }

    public GenerateSprintReportException(Exception innerException, GenerateSprintReportErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
    {
        ErrorCode = errorCode;
    }

    public GenerateSprintReportException(GenerateSprintReportErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public GenerateSprintReportException(GenerateSprintReportErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException(nameof(info));
        }

        base.GetObjectData(info, context);
        info.AddValue(nameof(ErrorCode), ErrorCode);
    }

    private GenerateSprintReportException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        var errorCode = info.GetValue(nameof(ErrorCode), typeof(GenerateSprintReportErrorCode)) ?? GenerateSprintReportErrorCode.Unknown;
        ErrorCode = (GenerateSprintReportErrorCode)errorCode;
    }
}