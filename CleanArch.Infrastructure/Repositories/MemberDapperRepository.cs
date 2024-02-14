using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Repositories
{
    public class MemberDapperRepository : IMemberDapperRepository
    {
        private readonly IDbConnection _connection;

        public MemberDapperRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Member> GetMemberById(int id)
        {
            string query = "SELECT * FROM Members WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Member>(query, new { Id = id });
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            string query = "SELECT * FROM Members";
            return await _connection.QueryAsync<Member>(query);
        }
    }
}
