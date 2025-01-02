using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync();
    }
}
