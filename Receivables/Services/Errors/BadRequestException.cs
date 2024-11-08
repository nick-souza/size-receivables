namespace API.Services.Errors;

public class BadRequestException(string message) : Exception(message);