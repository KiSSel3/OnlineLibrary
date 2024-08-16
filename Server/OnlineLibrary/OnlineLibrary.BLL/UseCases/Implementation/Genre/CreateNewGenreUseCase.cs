using MapsterMapper;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Genre;

public class CreateNewGenreUseCase : ICreateNewGenreUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateNewGenreUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(string genreName, CancellationToken cancellationToken = default)
    {
        var newGenre = new GenreEntity() { Name = genreName };
        
        await _unitOfWork.GetBaseRepository<GenreEntity>().CreateAsync(newGenre, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}