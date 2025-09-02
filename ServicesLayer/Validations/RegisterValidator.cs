using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Exceptions;

namespace Business.ServicesLayer.Validations
{
    public class RegisterValidator : IRegisterValidator
    {
        public void Validate(RegisterDto registerDto)
        {
            if(registerDto != null)
            {
                var errors = new List<string>();
                registerDto.Role = registerDto.Role.ToLower();
                if(registerDto.FirstName.Length < 3)
                {
                    errors.Add("The first name can't be less than 3 characters");
                }
                if(registerDto.LastName.Length < 3)
                {
                    errors.Add("The last name can't be less than 3 characters");
                }
                if(!registerDto.Email.Contains("@") || !registerDto.Email.Contains("."))
                {
                    errors.Add("Enter a valid Eamil");
                }
                if(registerDto.Password.Length < 8)
                {
                    errors.Add("Password can't be less than 8 charchters");
                }
                if(registerDto.Password != registerDto.PasswordConfirmation)
                {
                    errors.Add("Passwords mismatch");
                }
                if(!registerDto.Role.Equals("admin") && !registerDto.Role.Equals("author") && !registerDto.Role.Equals("reader") )
                {
                    errors.Add("Role not available");
                }
                if (errors.Any())
                {
                    throw new ValidationException(string.Join(" | ", errors));
                }
            } else
            {
                throw new ValidationException("Please fill in the data");
            }
                
        }
    }
}
