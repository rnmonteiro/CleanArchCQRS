using CleanArch.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _unitOfWork.MemberRepository.GetMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembers(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberById(id);
            return member != null ? Ok(member) : NotFound("Member not found.");
        }
    }
}
