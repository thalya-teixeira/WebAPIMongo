using System.Collections.Generic;
using System.Linq;
using ExemploWebApiMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMongo.Models;
using WebAPIMongo.Service;

namespace WebAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService; //readonly protecao
        private readonly AddressService _addressService; //coloquei apos fazer a classe address por fazer relacionamento com address em client

        #region Method
        public ClientController(ClientService clientService, AddressService addressService)
        {
            _clientService = clientService;
            _addressService = addressService; //adc depois de fazer o relacionamento
        }
        #endregion

        #region Lista Todos Client
        [HttpGet] 
        public ActionResult<List<Client>> Get() => _clientService.Get(); //get sem parametro traz todo mundo
        #endregion

        #region Lista Client Específico Pelo ID
        [HttpGet("{id:length(24)}", Name = "GetClient")] // porque id no mongo é uma string haschcode de 24 caracteres
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);

            if (client == null)
                return NotFound();

            return client;
        }
        #endregion

        #region Lista Pelo Nome 
        [HttpGet("{name}", Name = "GetName")]
        public ActionResult<Client> GetName(string name)
        {
            var client = _clientService.GetByName(name);
            if (client == null) return NotFound();//404

            return Ok(client);
        }
        [HttpGet("address/{idaddress:length(24)}", Name = "GetClientAddress")]
        public ActionResult<Client> GetAddress(string idaddress)
        {
            var client = _clientService.GetByAddress(idaddress);
            if (client == null) return NotFound();

            return Ok(client);
        }
        #endregion

        #region Insert Client
        [HttpPost] 
        public ActionResult<Client> Create(Client client)
        {
            Address address = _addressService.Create(client.Address); //chamando funcao addressService entao nao preciso acrescentar em clientservice
            client.Address = address;

            _clientService.Create(client);//chamo service do client para inserir 
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
            
        }
        #endregion

        #region Update Client
        [HttpPut] 
        public ActionResult<Client> Update(string id,Client clientIn)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound();

            clientIn.Id = id;
            _clientService.Update(clientIn.Id, clientIn);

            //return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
            //client = _clientService.Get(id);    
            //return Ok(client);

            return NoContent(); //retornar NoContente no update - 204
            //tenho que passar o obj completo pois se nao o mongo nao altera

        }
        #endregion

        #region Delete Client
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound();

            _clientService.Remove(client);
            return NoContent(); // para deletar retornar NoContent - 204
        }
        #endregion

    }
}
