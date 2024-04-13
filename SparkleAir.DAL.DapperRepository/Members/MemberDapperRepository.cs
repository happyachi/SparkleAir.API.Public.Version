using Dapper;
using Microsoft.Data.SqlClient;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.DapperRepository.Members
{
    //public class MemberDapperRepository : IMemberRepository
    public class MemberDapperRepository 

    {
        private string _connectionString;
        public MemberDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MemberEntity Get(MemberGetCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetAllMembersIdByMemberClassId(int memberClassId)
        {
            throw new NotImplementedException();
        }

        public List<MemberEntity> Search(MemberSearchCriteria criteria)
        {
            string sql = @"SELECT * FROM Members";

            using (var conn = new SqlConnection(_connectionString))
            {
                List<MemberEntity> entity = conn.Query<MemberEntity>(sql).ToList();
                return entity;
            }
        }

        public void Update(MemberEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
