using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Members
{
    public interface IMemberRepository
    {
        List<MemberEntity> Search(MemberSearchCriteria criteria);

        MemberEntity Get(MemberGetCriteria criteria);

        void Update(MemberEntity entity);

        /// <summary>
        /// 給會員等級id，回傳該會員等級的所有會員id
        /// </summary>
        /// <param name="memberClassId">會員等級Id</param>
        /// <returns>IEnumerable的會員Id</returns>
        IEnumerable<int> GetAllMembersIdByMemberClassId(int memberClassId);

        int Register(MemberEntity entity);

        bool ActiveRegister(int memberId, string confirmCode);

    }
}
