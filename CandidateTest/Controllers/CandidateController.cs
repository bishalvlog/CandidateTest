using Candidate.Application.DTOs.Candidate;
using Candidate.Application.Interfaces.Services;
using Candidate.Application.Response;
using CandidateTest.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateTest.Controllers
{
    [Route("api/candidate")]
    public class CandidateController(ICandidateService candidateService) : BaseController<CandidateController>
    {
        [HttpPost]
        public IActionResult InsertUpdateCandidate([FromBody] CreateCandidateDto candidateDto)
        {
            bool isUpdate = candidateService.InsertCandidate(candidateDto);

            string message = isUpdate ? "Candidate inserted successfully." : "Candidate already exists.";

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = message,
                Result = true
            });
        }
    }
}
