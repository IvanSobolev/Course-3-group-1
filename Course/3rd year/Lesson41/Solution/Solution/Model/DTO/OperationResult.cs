namespace Solution.Model.Structures;

public class OperationResult (bool isSuccess, string? error = default)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string? ErrorMessage { get; set; } = error;
}