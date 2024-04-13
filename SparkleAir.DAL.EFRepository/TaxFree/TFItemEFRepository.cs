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
    public class TFItemEFRepository : ITFRepository
    {
        private AppDbContext _db;

        public TFItemEFRepository(AppDbContext db)
        {
            _db = db;
        }

        public int Create(TFItemEntity entity)
        {
            var tfModel = new Tfitem();
            tfModel.Id = entity.Id;
            tfModel.Name = entity.Name;
            tfModel.SerialNumber = entity.SerialNumber;
            tfModel.Image = entity.Image;
            tfModel.Quantity = entity.Quantity;
            tfModel.UnitPrice = entity.UnitPrice;
            tfModel.Description = entity.Description;
            tfModel.IsPublished = entity.IsPublished;
            tfModel.TfcategoriesId = entity.TfcategoriesId;

            _db.Tfitems.Add(tfModel);
            _db.SaveChanges();
            return entity.Id;
        }

        public void Update(TFItemEntity entity)
        {

            _db.Tfitems.Find(entity.Id);

            var tfModel = _db.Tfitems.Find(entity.Id);
            tfModel.Id = entity.Id;
            tfModel.Name = entity.Name;
            tfModel.SerialNumber = entity.SerialNumber;
            tfModel.Image = entity.Image;
            tfModel.Quantity = entity.Quantity;
            tfModel.UnitPrice = entity.UnitPrice;
            tfModel.Description = entity.Description;
            tfModel.IsPublished = entity.IsPublished;
            tfModel.TfcategoriesId = entity.TfcategoriesId;

            _db.SaveChanges();

        }

        public void Delete(int id)
        {

            var tfModel = _db.Tfitems.Find(id);
            var wishModel = _db.Tfwishlists.Where(x => x.TfitemsId == id);
            _db.Tfwishlists.RemoveRange(wishModel);
            _db.Tfitems.Remove(tfModel);
            _db.SaveChanges();

        }

        //public void DeleteTFWishlist(int id)
        //{
        //    var db =new AppDbContext();

        //    db.SaveChanges();
        //}




        public List<TFItemEntity> Get()
        {
            var itemget = _db.Tfitems.AsNoTracking()
                                    .Include(x => x.Tfcategories)
                                    .Select(x => new TFItemEntity
                                    {
                                        Id = x.Id,
                                        TFCategoriesName = x.Tfcategories.Category,
                                        Name = x.Name,
                                        SerialNumber = x.SerialNumber,
                                        Image = x.Image,
                                        Quantity = x.Quantity,
                                        UnitPrice = x.UnitPrice,
                                        Description = x.Description,
                                        IsPublished = x.IsPublished,
                                        TfcategoriesId = x.TfcategoriesId
                                    })
                                    .ToList();
            return itemget;
        }



        public List<TFItemEntity> Search(TFItemEntity entity)
        {

            List<TFItemEntity> itemList = _db.Tfitems.AsNoTracking()
                                        //.Where(x => x.Name.Contains(name))
                                        .Include(x => x.Tfcategories)
                                        .OrderBy(x => x.Id)
                                        .Select(x => new TFItemEntity
                                        {
                                            Id = x.Id,
                                            TFCategoriesName = x.Tfcategories.Category,
                                            Name = x.Name,
                                            SerialNumber = x.SerialNumber,
                                            Image = x.Image,
                                            Quantity = x.Quantity,
                                            UnitPrice = x.UnitPrice,
                                            Description = x.Description,
                                            IsPublished = x.IsPublished,
                                            TfcategoriesId = x.TfcategoriesId
                                        })
                                        .ToList();

            return itemList;
        }

        public TFItemEntity Getid(int id)
        {
            var get = _db.Tfitems
                 .Include(item => item.Tfcategories) // 加入這行來載入 Tfcategories
                 .FirstOrDefault(item => item.Id == id); ;

            if (get == null)
            {
                
                throw new Exception("Item not found");
            }
            TFItemEntity getitem = new TFItemEntity()
            {
                Id = get.Id,
                TFCategoriesName = get.Tfcategories.Category,
                Name = get.Name,
                SerialNumber = get.SerialNumber,
                Image = get.Image,
                Quantity = get.Quantity,
                UnitPrice = get.UnitPrice,
                Description = get.Description,
                IsPublished = get.IsPublished,
                TfcategoriesId = get.TfcategoriesId
            };

            return getitem;
        }

        //public void Delete(int id)
        //{
        //    var ItemGet = db.TFItems.Find(id);
        //    TFItemEntity item = new TFItemEntity
        //    {
        //        Id = ItemGet.Id,
        //        Name = ItemGet.Name,
        //        SerialNumber = ItemGet.SerialNumber,
        //        Image = ItemGet.Image,
        //        Quantity = ItemGet.Quantity,
        //        UnitPrice = ItemGet.UnitPrice,
        //        Description = ItemGet.Description,
        //        IsPublished = ItemGet.IsPublished

        //    };
        //    return item;

        //}

        public List<TFCategoryDto> GetCategories()
        {
            return _db.Tfcategories
                .Select(category => new TFCategoryDto
                {
                    Id = category.Id,
                    Category = category.Category
                })
                .ToList();
        }
        public TFCategoryEntity GetCategoryById(int id)
        {
            var get = _db.Tfcategories.Find(id);

            TFCategoryEntity getitem = new TFCategoryEntity()
            {
                Id = get.Id,
                Category = get.Category
            };

            return getitem;
        }
    }
}

