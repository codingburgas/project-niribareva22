namespace SchoolGradebook.Models;

// ViewModel used to display error information to the user
public class ErrorViewModel
{
    // The unique request ID for tracking the error
    public string? RequestId { get; set; }

    // Boolean helper to determine if the RequestId should be displayed
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}