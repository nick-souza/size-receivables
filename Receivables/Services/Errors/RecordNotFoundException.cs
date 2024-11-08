namespace API.Services.Errors;

public class RecordNotFoundException(string message) : Exception(message);