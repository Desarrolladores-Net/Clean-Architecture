using App.DTO;
using App.Presenters;
using App.UseCasePorts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllPeopleController
    {
        private readonly IGetAllPeopleInputPort InputPort;
        private readonly IGetAllPeopleOutputPort OutputPort;

        public GetAllPeopleController(IGetAllPeopleInputPort inputPort, IGetAllPeopleOutputPort outputPort)
        {
            InputPort = inputPort;
            OutputPort = outputPort;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonDTO>> GetAllPerson()
        {
            await InputPort.Handle();
            return ((IPresenter<IEnumerable<PersonDTO>>)OutputPort).Content;
        }

    }
}
