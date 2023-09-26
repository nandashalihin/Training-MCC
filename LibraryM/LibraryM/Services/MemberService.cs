using LibraryM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryM.Services
{
    public class MemberService
    {
        private readonly DataService _dataService;

        public MemberService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void AddMember(Member member)
        {
            _dataService.Members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return _dataService.Members;
        }

        public void EditMember(int membershipNumber, Member updatedMember)
        {
            var member = _dataService.Members.Find(m => m.MembershipNumber == membershipNumber);
            if (member != null)
            {
                member.Name = updatedMember.Name;
                member.Email = updatedMember.Email;
                member.MembershipNumber = updatedMember.MembershipNumber;
            }
        }

        public Member DeleteMember(int membershipNumber)
        {
            var memberToDelete = _dataService.Members.FirstOrDefault(m => m.MembershipNumber == membershipNumber);
            
                _dataService.Members.Remove(memberToDelete);
            return memberToDelete;
        }
    }
}
