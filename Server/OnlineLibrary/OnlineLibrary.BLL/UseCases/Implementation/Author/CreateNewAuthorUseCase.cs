using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Author;

public class CreateNewAuthorUseCase : ICreateNewAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNewAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(AuthorCreateRequestDTO authorRequestDTO, CancellationToken cancellationToken = default)
    {
        var newAuthor = _mapper.Map<AuthorEntity>(authorRequestDTO);

        await _unitOfWork.GetBaseRepository<AuthorEntity>().CreateAsync(newAuthor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}