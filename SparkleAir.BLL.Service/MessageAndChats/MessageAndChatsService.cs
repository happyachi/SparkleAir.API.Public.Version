using Azure.Messaging;
using Microsoft.EntityFrameworkCore;
using SparkleAir.Infa.Dto.MessageAndChats;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.MessageAndChats
{
    public class MessageAndChatsService
    {
        private AppDbContext _db;

        public MessageAndChatsService(AppDbContext db)
        {
            _db = db;
        }

        public List<MessageAndChatDto> GetMessageByMemberId(int memberId)
        {
            var meaageList = _db.Messages
                .Where(m => m.MemberId == memberId)
                .Select(m => new MessageAndChatDto
                {
                    Id = m.Id,
                    MessageBoxId = m.MessageBoxId,
                    SendTime = m.SendTime,
                    MessageContent = m.MessageContent,
                    MemberId = m.MemberId,
                    CompanyStaffId = m.CompanyStaffId,
                    IsReaded = m.IsReaded,
                    ReadedTime = m.ReadedTime,
                })
                .OrderBy(m => m.SendTime)
                .ToList();

            return meaageList;
        }

        public void CreateMessage(MessageAndChatDto dto)
        {
            var message = new Message()
            {
                MessageBoxId = 1,
                SendTime = DateTime.Now,
                MessageContent = dto.MessageContent,
                MemberId = dto.MemberId,
                IsReaded = false,
                CompanyStaffId = (dto.CompanyStaffId == 0)? null: dto.CompanyStaffId
            };
            
            _db.Messages.Add(message);
            _db.SaveChanges();
        }

        public List<MessageMemberInfoDto> GetAllMembers()
        {
            var memberIdList = _db.Messages
                .Select(x => new MessageMemberInfoDto
                 {
                     MemberId = (int)x.MemberId,
                     MemberName = $"{x.Member.EnglishFirstName} {x.Member.EnglishLastName}"
                 })
                .Distinct()
                .ToList();

            return memberIdList;
        }
    }
}
