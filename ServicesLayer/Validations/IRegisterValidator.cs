using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Validations
{
    public interface IRegisterValidator
    {
        void Validate(RegisterDto registerDto);
    }
}
