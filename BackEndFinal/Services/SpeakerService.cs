using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly IRepository<Speaker> _speakerRepository;

        public SpeakerService(IRepository<Speaker> speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        public Task AddSpeakerAsync(Speaker Speaker)
        {
            if(Speaker == null) throw new ArgumentNullException(nameof(Speaker));
            return _speakerRepository.AddAsync(Speaker);
        }

        public Task DeleteSpeakerAsync(Speaker Speaker)
        {
            if (Speaker == null) throw new ArgumentNullException(nameof(Speaker));
            return _speakerRepository.DeleteAsync(Speaker);
        }

        public  IQueryable<Speaker> GetAllSpeakerQuery()
        {
            return   _speakerRepository.GetAllQuery();
        }

        public async Task<List<Speaker>> GetAlSpeakerAsync(int skip, int take, params Expression<Func<Speaker, object>>[] includes)
        {
            return await _speakerRepository.GetAllAsync(skip,take, includes);
        }

        public async Task<Speaker> GetSpeakerByIdAsync(int? id, params Expression<Func<Speaker, object>>[] includes)
        {
            return await _speakerRepository.GetByIdAsync(id, includes);
        }

        public Task UpdateSpeakerAsync(Speaker Speaker)
        {
            if(_speakerRepository == null) throw new ArgumentNullException(nameof(Speaker));
            return _speakerRepository.UpdateAsync(Speaker);
        }
    }
}
