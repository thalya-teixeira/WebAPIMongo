using ExemploWebApiMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPIMongo.Models;
using WebAPIMongo.Service;

namespace ExemploWebApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        private readonly ClientService _clientService;

        #region Method
        public AddressController(AddressService addressServices, ClientService clientService)
        {
            _addressService = addressServices;
            _clientService = clientService;
        }
        #endregion

        #region Lista Todos Address
        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressService.Get();
        #endregion

        #region Lista Address Específico 
        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressService.Get(id);

            if (address == null)
                return NotFound();

            //var client = _clientService.Get(); //lista de clientes

            //var c = client.First(x => x.Address.Id == id);

            return address;
        }
        #endregion

        #region Insert Address
        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            _addressService.Create(address);
            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address);
        }
        #endregion

        #region Update Address
        [HttpPut]
        public ActionResult<Address> Update(string id, Address addressIn)
        {
            var address = _addressService.Get(id); //apenas verificacao se o endereço existe

            if (address == null)
                return NotFound();


            addressIn.Id = id;

            _addressService.Update(id, addressIn); //manupilacao do obj criado

            return CreatedAtRoute("GetAddress", new { id = addressIn.Id.ToString() }, addressIn);
        }
        #endregion

        #region Delete Address
        [HttpDelete]
        public ActionResult Remove(string id)
        {
            var address = _addressService.Get(id); //verificacao se o obj existe, se sim ele traz

            if (address == null)
                return NotFound();

            _addressService.Remove(address);

            return NoContent();
        }
        #endregion

    }
}