using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.TaxFree
{
    public interface ITFWishlist
    {
        List<TFWishlistsEntity> Get();

        List<TFWishlistsEntity> Search(TFWishlistsEntity entity);

        void Update(TFWishlistsEntity entity);

        int Create(TFWishlistsEntity entity);

        void Delete(int id);

        List<TFWishlistsEntity> Getid(int memberid);

        List<TFWishlistsEntity> GetByMemberIdAndItemId(int memberid, int itemid);
    }
}
