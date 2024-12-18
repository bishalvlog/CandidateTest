using CandidateWeb.Models.Requests;
using Microsoft.AspNetCore.Components;

namespace CandidateWeb.Pages
{
    public partial class Candidate : ComponentBase
    {

        #region Create Update Candidate
        private CreateCandidateDto candidateDto { get; set; } = new();

        private string successMessage; 

        private string errorMessage;

        private bool isSuccessVisible;

        private bool isErrorVisible;

        private bool IsCreateUpdateModalOpen { get; set; }

        private async Task HandleValidSubmit()
        {
            try
            {
                var result = await CandidateService.InsertUpdate(candidateDto);
                if (result != null && result.Result.HasValue && result.Result.Value)
                {
                    // Handle success
                    successMessage = result.Message;
                    isSuccessVisible = true;
                    isErrorVisible = false;
                }
                else
                {
                    errorMessage = result.Message ?? "Operation failed";
                    isErrorVisible = true;
                    isSuccessVisible = false;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
        #endregion

    }
}