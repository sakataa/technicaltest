namespace RobotSimulation.Console.Models
{
    public record OperationResult
    {
        public Location? Value { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public string ReportMessage { get; set; } = string.Empty;

        public bool IsSuccessful => string.IsNullOrWhiteSpace(ErrorMessage);

        public bool ShouldStopProcessing { get; set; }
    }
}