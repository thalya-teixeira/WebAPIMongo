using MongoDB.Driver;
using System.Collections.Generic;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace ExemploWebApiMongo.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _addressServices;

        public AddressService(IDatabaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DatabaseName);
            _addressServices = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public Address Create(Address address)
        {
            _addressServices.InsertOne(address);
            return address;
        }

        public List<Address> Get() => _addressServices.Find(address => true).ToList(); // retorna todos

        public Address Get(string id) => _addressServices.Find<Address>(address => address.Id == id).FirstOrDefault(); //retorna o primeiro que achar

        public void Update(string id, Address addressIn)
        {
            _addressServices.ReplaceOne(address => address.Id == id, addressIn);
        }

        public void Remove(Address addressIn) => _addressServices.DeleteOne(address => address.Id == addressIn.Id);

    }
}