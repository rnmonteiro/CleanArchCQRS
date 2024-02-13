using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly AppDbContext db;

        public MemberRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<Member> AddMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            await db.Members.AddAsync(member);
            return member;
        }

        public Task<Member> DeleteMember(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetMemberById(int id)
        {
            var member = await db.Members.FindAsync(id);

            if (member is null)
                throw new ArgumentNullException("Member not found");

            return member;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            var memberlist = await db.Members.ToListAsync();
            return memberlist ?? Enumerable.Empty<Member>();
        }

        public void UpdateMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            db.Members.Update(member);
        }
    }
}
