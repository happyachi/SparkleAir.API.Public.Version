using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members
{
    public class MemberClassService
    {
        private readonly IMemberClassRepository _repo;

        public MemberClassService(IMemberClassRepository repo)
        {
            _repo = repo;
        }

        public List<MemberClassDto> Search()
        {
            var entities = _repo.Search();
            var dots = entities.Select(m => new MemberClassDto
            {
                Id = m.Id,
                Name = m.Name,
                ClassOrder = m.ClassOrder,
                MileageStart = m.MileageStart,
                MileageEnd = m.MileageEnd
            }).ToList();

            return dots;
        }

        public void Create(MemberClassDto dto)
        {
            var entity = new MemberClassEntity
            {
                Name = dto.Name,
                ClassOrder = dto.ClassOrder,
                MileageStart = dto.MileageStart,
                MileageEnd = dto.MileageEnd
            };

            _repo.Create(entity);
        }

        public MemberClassDto Get(int id)
        {
            var eneity = _repo.Get(id);
            var dto = new MemberClassDto
            {
                Id = eneity.Id,
                Name = eneity.Name,
                ClassOrder = eneity.ClassOrder,
                MileageStart = eneity.MileageStart,
                MileageEnd = eneity.MileageEnd
            };
            return dto;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public void Update(MemberClassDto dto)
        {
            var entity = new MemberClassEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                ClassOrder = dto.ClassOrder,
                MileageStart = dto.MileageStart,
                MileageEnd = dto.MileageEnd
            };

            _repo.Update(entity);
        }
    }
}
