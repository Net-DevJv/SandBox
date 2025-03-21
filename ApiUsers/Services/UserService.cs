using ApiUsers.DTO;
using ApiUsers.Models;
using ApiUsers.Services.Interfaces;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiUsers.Services
{
    public class UserService : IUserInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ListUserDTO>>> CreateUser(CreateUserDTO createUserDTO)
        {
            ResponseModel<List<ListUserDTO>> response = new ResponseModel<List<ListUserDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                var userBase = await connection.ExecuteAsync(
                    "INSERT INTO ApiUserProfiles " +
                    "(FullName, Email, Position, Wage, CPF, Situation, Password) " +
                    "values (@FullName, @Email, @Position, @Wage, @CPF, @Situation, @Password)", createUserDTO);

                if (userBase == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var users = await ListUsers(connection);
                var userMaped = _mapper.Map<List<ListUserDTO>>(users);

                response.Data = userMaped;
                response.Message = "Usuários listados com sucesso!";
            }

            return response;
        }

        private static async Task<IEnumerable<User>> ListUsers(SqlConnection connection)
        {
            return await connection.QueryAsync<User>("select * from ApiUserProfiles");
        }

        public async Task<ResponseModel<ListUserDTO>> GetUserById(int userId)
        {
            ResponseModel<ListUserDTO> response = new ResponseModel<ListUserDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                var userBase = await connection.QueryFirstOrDefaultAsync<User>("select * from ApiUserProfiles where id = @Id", new {Id = userId});

                if (userBase == null)
                {
                    response.Message = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }

                var userMaped = _mapper.Map<ListUserDTO>(userBase);

                response.Data = userMaped;
                response.Message = "Usuário localizado com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<ListUserDTO>>> GetUsers()
        {
            ResponseModel<List<ListUserDTO>> response = new ResponseModel<List<ListUserDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                var userBase = await connection.QueryAsync<User>("select * from ApiUserProfiles");

                if (userBase.Count() == 0)
                {
                    response.Message = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }

                var userMaped = _mapper.Map<List<ListUserDTO>>(userBase);

                response.Data = userMaped;
                response.Message = "Usuários localizados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<ListUserDTO>>> EditUser(EditUserDTO editUserDTO)
        {
            ResponseModel<List<ListUserDTO>> response = new ResponseModel<List<ListUserDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                var userBase = await connection.ExecuteAsync(
                    "update ApiUserProfiles set " +
                    "FullName = @FullName, " +
                    "Email = @Email, " +
                    "Position = @Position, " +
                    "Wage = @Wage, " +
                    "CPF = @CPF, " +
                    "Situation = @Situation " +
                    "where Id = @Id ", editUserDTO);

                if (userBase == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                    return response;
                }

                var users = await ListUsers(connection);
                var userMaped = _mapper.Map<List<ListUserDTO>>(users);

                response.Data = userMaped;
                response.Message = "Usuários listados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<ListUserDTO>>> RemoveUser(int userId)
        {
            ResponseModel<List<ListUserDTO>> response = new ResponseModel<List<ListUserDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                var userBase = await connection.ExecuteAsync("delete from ApiUserProfiles where id = @Id", new {Id = userId});

                if (userBase == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                    return response;
                }

                var users = await ListUsers(connection);
                var userMaped = _mapper.Map<List<ListUserDTO>>(users);

                response.Data = userMaped;
                response.Message = "Usuários listados com sucesso!";
            }

            return response;
        }
    }
}
