using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.TaxFree
{
    public class TFWishlistService
    {
        private readonly ITFWishlist _repo;
        public TFWishlistService(ITFWishlist repo)
        {
            _repo = repo;
        }

        public List<TFWishlistDto> Get()
        {
            List<TFWishlistsEntity> result = _repo.Get();
            List<TFWishlistDto> list = result.Select(x => new TFWishlistDto
            {
                Id = x.Id,
                MemberId = x.MemberId,
                MemberPassportNumber = x.MemberPassportNumber,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                TFItemsName = x.TFItemsName,
                TFItemsImage = x.TFItemsImage,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                TFItemsSerialNumber = x.TFItemsSerialNumber,
                TFItemsId = x.TFItemsId

            }).ToList();
            return list;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public int Create(TFWishlistDto dto)
        {
            TFWishlistsEntity entity = new TFWishlistsEntity
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                MemberPassportNumber = dto.MemberPassportNumber,
                MemberChineseFirstName = dto.MemberChineseFirstName,
                MemberChineseLastName = dto.MemberChineseLastName,
                MemberEnglishFirstName = dto.MemberEnglishFirstName,
                MemberEnglishLastName = dto.MemberEnglishLastName,
                MemberPhone = dto.MemberPhone,
                MemberEmail = dto.MemberEmail,
                TFItemsName = dto.TFItemsName,
                TFItemsImage = dto.TFItemsImage,
                TFItemsQuantity = dto.TFItemsQuantity,
                TFItemsUnitPrice = dto.TFItemsUnitPrice,
                TFItemsSerialNumber = dto.TFItemsSerialNumber,
                TFItemsId = dto.TFItemsId
            };
            return entity.Id;
        }

        public void Update(TFWishlistDto dto)
        {
            TFWishlistsEntity entity = new TFWishlistsEntity
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                MemberPassportNumber = dto.MemberPassportNumber,
                MemberChineseFirstName = dto.MemberChineseFirstName,
                MemberChineseLastName = dto.MemberChineseLastName,
                MemberEnglishFirstName = dto.MemberEnglishFirstName,
                MemberEnglishLastName = dto.MemberEnglishLastName,
                MemberPhone = dto.MemberPhone,
                MemberEmail = dto.MemberEmail,
                TFItemsName = dto.TFItemsName,
                TFItemsImage = dto.TFItemsImage,
                TFItemsQuantity = dto.TFItemsQuantity,
                TFItemsUnitPrice = dto.TFItemsUnitPrice,
                TFItemsSerialNumber = dto.TFItemsSerialNumber,
                TFItemsId = dto.TFItemsId
            };
            _repo.Update(entity);
        }

        public List<TFWishlistDto> Getid(int id)
        {

            List<TFWishlistsEntity> result = _repo.Getid(id);
            List<TFWishlistDto> list = result.Select(x => new TFWishlistDto
            {
                Id = x.Id,
                MemberId = x.MemberId,
                MemberPassportNumber = x.MemberPassportNumber,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                TFItemsName = x.TFItemsName,
                TFItemsImage = x.TFItemsImage,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                TFItemsSerialNumber = x.TFItemsSerialNumber,
                TFItemsId = x.TFItemsId

            }).ToList();
            return list;

        }

        public List<TFWishlistDto> GetByMemberIdAndItemId(int memberid,int itemid)
        {

            List<TFWishlistsEntity> result = _repo.GetByMemberIdAndItemId(memberid,itemid);
            List<TFWishlistDto> list = result.Select(x => new TFWishlistDto
            {
                Id = x.Id,
                MemberId = x.MemberId,
                MemberPassportNumber = x.MemberPassportNumber,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                TFItemsName = x.TFItemsName,
                TFItemsImage = x.TFItemsImage,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                TFItemsSerialNumber = x.TFItemsSerialNumber,
                TFItemsId = x.TFItemsId

            }).ToList();
            return list;

        }
    }

}

