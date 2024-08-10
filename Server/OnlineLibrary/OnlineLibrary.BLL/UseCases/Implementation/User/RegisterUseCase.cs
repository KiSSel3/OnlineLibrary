using MapsterMapper;
using Microsoft.Extensions.Configuration;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.User;

public class RegisterUseCase : IRegisterUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public RegisterUseCase(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordService passwordService, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<TokenResponseDTO> ExecuteAsync(UserRequestDTO userRequestDto, CancellationToken cancellationToken = default)
    {
        await ValidateUserDoesNotExistAsync(userRequestDto.Login, cancellationToken);

        var newUser = await CreateNewUserAsync(userRequestDto, cancellationToken);
        await AssignRoleToUserAsync("User", newUser.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = await GenerateAccessTokenAsync(newUser, cancellationToken);
        
        return new TokenResponseDTO() { RefreshToken = newUser.RefreshToken, AccessToken = accessToken };
    }
    
    private async Task ValidateUserDoesNotExistAsync(string login, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.GetCustomRepository<IUserRepository>().GetByLoginAsync(login, cancellationToken);
        if (existingUser != null)
        {
            throw new AuthenticationException("User with this login already exists.");
        }
    }
    
    private async Task<UserEntity> CreateNewUserAsync(UserRequestDTO userDto, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<UserEntity>(userDto);
        newUser.PasswordHash = _passwordService.HashPassword(userDto.Password);
        newUser.RefreshToken = _tokenService.GenerateRefreshToken();
        newUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenExpirationDays").Get<int>());
        
        await _unitOfWork.GetCustomRepository<IUserRepository>().CreateAsync(newUser, cancellationToken);
        
        return newUser;
    }
    
    private async Task AssignRoleToUserAsync(string roleName, Guid userId, CancellationToken cancellationToken)
    {
        var roleRepository = _unitOfWork.GetCustomRepository<IRoleRepository>(); 
        
        var role = await roleRepository.GetByNameAsync(roleName, cancellationToken);
        if (role == null)
        {
            throw new EntityNotFoundException($"{roleName} role not found.");
        }

        await roleRepository.SetRoleToUserAsync(new UserRoleEntity() { UserId = userId, RoleId = role.Id }, cancellationToken);
    }
    
    private async Task<string> GenerateAccessTokenAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var rolesByUser = await _unitOfWork.GetCustomRepository<IRoleRepository>().GetRolesByUserIdAsync(user.Id, cancellationToken);
        var claims = _tokenService.CreateClaims(user, rolesByUser.ToList());
        return _tokenService.GenerateAccessToken(claims);
    }
}