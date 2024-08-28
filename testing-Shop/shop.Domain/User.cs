namespace shop.Domain;
using System;
using System.Net.Mail;

public class User
{
    private string _mail;
    private string _address;
    private string _password;

    public User()
    {
        _mail = string.Empty;
        _address = string.Empty;
        _password = string.Empty;
    }

    public int Id { get; set; }

    public string Mail
    {
        get => _mail;
        set
        {
            ValidateMail(value);
            _mail = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            ValidateRequiredField(value, nameof(Address));
            _address = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            ValidateRequiredField(value, nameof(Password));
            _password = value;
        }
    }

    public DateTime? DeletionDate { get; set; }

    public void Delete()
    {
        DeletionDate = DateTime.Now;
    }

    private void ValidateMail(string mail)
    {
        if (string.IsNullOrEmpty(mail))
        {
            throw new RequiredValueIsEmptyException("User mail cannot be empty.");
        }
        if (!IsValidEmail(mail))
        {
            throw new InvalidValueException("User mail is not valid.");
        }
    }

    private void ValidateRequiredField(string value, string fieldName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new RequiredValueIsEmptyException($"User {fieldName} cannot be empty.");
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not User other) return false;

        return Id == other.Id &&
               Mail == other.Mail &&
               Address == other.Address &&
               Password == other.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Mail, Address, Password);
    }
}

[Serializable]
internal class InvalidValueException : Exception
{
    public InvalidValueException() { }

    public InvalidValueException(string? message) : base(message) { }

    public InvalidValueException(string? message, Exception? innerException) : base(message, innerException) { }
}

[Serializable]
internal class RequiredValueIsEmptyException : Exception
{
    public RequiredValueIsEmptyException() { }

    public RequiredValueIsEmptyException(string? message) : base(message) { }

    public RequiredValueIsEmptyException(string? message, Exception? innerException) : base(message, innerException) { }
}