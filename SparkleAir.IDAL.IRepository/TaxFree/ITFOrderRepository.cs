using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.TaxFree
{
    public interface ITFOrderRepository
    {
        List<TFOrderlistsEntity> Get();

        List<TFOrderlistsEntity> Search(TFOrderlistsEntity entity);

        void Update(TFOrderlistsEntity entity);

        int Create(TFOrderlistsEntity entity);

        void Delete(int id);

        TFOrderlistsEntity Getid(int id);

        int AddToCart(TFOrderlistsEntity entity);

        List<TFOrderlistsEntity> GetItemsByTicketDetailsId(int ticketDetailsId);
        List<TFOrderlistsEntity> GetSelectedItems(int ticketDetailsId, List<int> selectedOrderListIds);

        TFOrderlistsEntity UpdateQuantity(int id, int newQuantity);
        void UpdatePaid(TFOrderlistsEntity entity);

        TFOrderlistsEntity Getorderid(int id);


        

        



    }
}
