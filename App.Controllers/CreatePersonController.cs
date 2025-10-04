using App.DTO;
using App.Presenters;
using App.UseCasePorts;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePersonController
    {
        private readonly ICreatePersonInputPort InputPort;
        private readonly ICreatePersonOutputPort OutputPort;

        public CreatePersonController(ICreatePersonInputPort inputPort, ICreatePersonOutputPort outputPort)
        {
            InputPort = inputPort;
            OutputPort = outputPort;
        }

        [HttpPost]
        public async Task<PersonDTO> CreatePerson(CreatePersonDTO person)
        {
            await InputPort.Handle(person);

            return ((IPresenter<PersonDTO>)OutputPort).Content;
        }

    }
}