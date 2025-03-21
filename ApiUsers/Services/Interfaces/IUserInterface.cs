using ApiUsers.DTO;
using ApiUsers.Models;

namespace ApiUsers.Services.Interfaces
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<ListUserDTO>>> GetUsers();

        Task<ResponseModel<ListUserDTO>> GetUserById(int userId);

        Task<ResponseModel<List<ListUserDTO>>> CreateUser(CreateUserDTO createUserDTO);

        Task<ResponseModel<List<ListUserDTO>>> EditUser(EditUserDTO editUserDTO);

        Task<ResponseModel<List<ListUserDTO>>> RemoveUser(int userId);
    }
}
