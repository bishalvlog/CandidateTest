using Candidate.Application.DTOs.Candidate;
using Candidate.Application.Interfaces.Services;
using Candidate.Application.Response;
using CandidateTest.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateTest.Controllers
{
    public class CandidateController(ICandidateService candidateService) : BaseController<CandidateController>
    {
        [HttpPost]
        public IActionResult InsertUpdateCandidate(CreateUpdateCandidateDto candidateDto)
        {
            bool isUpdate = candidateService.InsertUpdateCandidate(candidateDto);

            string message = isUpdate ? "Candidate updated successfully." : "Candidate inserted successfully.";

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = message,
                Result = true
            });
        }
    }
}
