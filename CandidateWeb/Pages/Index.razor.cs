using Microsoft.AspNetCore.Components;

namespace CandidateWeb.Pages
{
    public partial class Index :ComponentBase
    {

        protected async override void OnInitialized()
        {
            Nav.NavigateTo("/candidate");
        }
    }
}