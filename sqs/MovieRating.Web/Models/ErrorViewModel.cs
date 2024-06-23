namespace MovieRating.Web.Models;

/// <summary>
/// Class <c>ErrorViewModel</c> represents the model for displaying error information.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Gets or sets the request ID associated with the error.
    /// </summary>
    /// <value>The request ID as a string.</value>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets a value indicating whether the request ID should be shown.
    /// </summary>
    /// <value><c>true</c> if the request ID should be shown; otherwise, <c>false</c>.</value>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}