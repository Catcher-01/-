using System.Threading.Tasks;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;
using HealthSupervision.DAOs;
using AutoMapper;

namespace HealthSupervision.Services
{
    public class HealthSnapshotService
    {
        private readonly IHealthSnapshotRepository _snapshotRepository;
        private readonly IMapper _mapper;

        public HealthSnapshotService(IHealthSnapshotRepository snapshotRepository, IMapper mapper)
        {
            _snapshotRepository = snapshotRepository;
            _mapper = mapper;
        }

        public async Task<HealthSnapshotDTO> GenerateSnapshotAsync(string userId)
        {
            var snapshot = await _snapshotRepository.GenerateSnapshotAsync(userId);
            return _mapper.Map<HealthSnapshotDTO>(snapshot);
        }
    }
}