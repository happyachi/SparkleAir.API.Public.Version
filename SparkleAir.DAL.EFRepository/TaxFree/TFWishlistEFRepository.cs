using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.TaxFree
{
    public class TFWishlistEFRepository : ITFWishlist
    {

        private AppDbContext _db;

        public TFWishlistEFRepository(AppDbContext db)
        {
            _db = db;
        }
        int ITFWishlist.Create(TFWishlistsEntity entity)
        {
            throw new NotImplementedException();
        }

        void ITFWishlist.Delete(int id)
        {
            throw new NotImplementedException();
        }

        List<TFWishlistsEntity> ITFWishlist.Get()
        {
            
            var getlist = _db.Tfwishlists.AsNoTracking()
                                        .Include(x => x.Tfitems)
                                        .Include(x => x.Member)
                                        .OrderBy(x => x.Id)
                                        .Select(x => new TFWishlistsEntity
                                        {
                                            Id = x.Id,
                                            MemberId = x.MemberId,
                                            MemberChineseFirstName = x.Member.ChineseFirstName,
                                            MemberChineseLastName = x.Member.ChineseLastName,
                                            MemberEnglishFirstName = x.Member.EnglishFirstName,
                                            MemberEnglishLastName = x.Member.EnglishLastName,
                                            MemberPhone = x.Member.Phone,
                                            MemberEmail = x.Member.Email,
                                            MemberPassportNumber = x.Member.PassportNumber,
                                            TFItemsName = x.Tfitems.Name,
                                            TFItemsSerialNumber = x.Tfitems.SerialNumber,
                                            TFItemsImage = x.Tfitems.Image,
                                            TFItemsQuantity = x.Tfitems.Quantity,
                                            TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                            TFItemsId = x.TfitemsId
                                        })
                                        .ToList();

            return getlist;
        }

        List<TFWishlistsEntity> ITFWishlist.Getid(int memberId)
        {
            var getList = _db.Tfwishlists.AsNoTracking()
                               .Include(x => x.Tfitems)
                               .Include(x => x.Member)
                               .Where(x => x.MemberId == memberId)
                               .Select(x => new TFWishlistsEntity
                               {
                                   Id = x.Id,
                                   MemberId = x.MemberId,
                                   MemberChineseFirstName = x.Member.ChineseFirstName,
                                   MemberChineseLastName = x.Member.ChineseLastName,
                                   MemberEnglishFirstName = x.Member.EnglishFirstName,
                                   MemberEnglishLastName = x.Member.EnglishLastName,
                                   MemberPhone = x.Member.Phone,
                                   MemberEmail = x.Member.Email,
                                   MemberPassportNumber = x.Member.PassportNumber,
                                   TFItemsName = x.Tfitems.Name,
                                   TFItemsSerialNumber = x.Tfitems.SerialNumber,
                                   TFItemsImage = x.Tfitems.Image,
                                   TFItemsQuantity = x.Tfitems.Quantity,
                                   TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                   TFItemsId = x.TfitemsId
                               })
                               .ToList();

            return getList;
        }

        List<TFWishlistsEntity> ITFWishlist.GetByMemberIdAndItemId(int memberid,int itemid)
        {
            var getlist = _db.Tfwishlists.AsNoTracking()
                               .Include(x => x.Tfitems)
                               .Include(x => x.Member)
                               .Where(x => x.MemberId == memberid && x.TfitemsId == itemid)
                               .Select(x => new TFWishlistsEntity

                               {
                                   Id = x.Id,
                                   MemberId = x.MemberId,
                                   MemberChineseFirstName = x.Member.ChineseFirstName,
                                   MemberChineseLastName = x.Member.ChineseLastName,
                                   MemberEnglishFirstName = x.Member.EnglishFirstName,
                                   MemberEnglishLastName = x.Member.EnglishLastName,
                                   MemberPhone = x.Member.Phone,
                                   MemberEmail = x.Member.Email,
                                   MemberPassportNumber = x.Member.PassportNumber,
                                   TFItemsName = x.Tfitems.Name,
                                   TFItemsSerialNumber = x.Tfitems.SerialNumber,
                                   TFItemsImage = x.Tfitems.Image,
                                   TFItemsQuantity = x.Tfitems.Quantity,
                                   TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                   TFItemsId = x.TfitemsId
                               })
                               .ToList();
            return getlist;

        }

        List<TFWishlistsEntity> ITFWishlist.Search(TFWishlistsEntity entity)
        {
            throw new NotImplementedException();
        }

        void ITFWishlist.Update(TFWishlistsEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
