using App.DTO;
using App.Entities.Interfaces;
using App.UseCasePorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UseCases.GetAllPeople
{
    public class GetAllPeopleInteractor : IGetAllPeopleInputPort
    {
        private readonly IPersonRepository Repository;
        private readonly IGetAllPeopleOutputPort OutputPort;

        public GetAllPeopleInteractor(IPersonRepository repository, IGetAllPeopleOutputPort outputPort)
        {
            Repository = repository;
            OutputPort = outputPort;
        }
       

        public Task Handle()
        {   //Usar automapper aqui
            var persons = Repository.GetAll().Select(x => new PersonDTO 
            {
                Name = x.Name,
                Id = x.Id
            });

            OutputPort.Handle(persons);
            return Task.CompletedTask;

        }
    }
}
