﻿using System.Collections.Generic;
using MongoDB.Driver;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace WebAPIMongo.Service
{
    public class ClientService //persistencia de banco de dados (seria o repository)
    {
        private readonly IMongoCollection<Client> _clients; 
        //private readonly IMongoCollection<Address> _address;

        public ClientService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clients = database.GetCollection<Client>(settings.ClientCollectionName);
        }
        
        public Client Create(Client client)
        {
            _clients.InsertOne(client);
            return client; //boa pratica retornar o que eu to inserindo
        }

        public List<Client> Get() => _clients.Find<Client>(client => true).ToList();

        public Client Get(string id) => _clients.Find<Client>(client => client.Id == id).FirstOrDefault();

        public void Update(string id, Client clientIn)
        {
            _clients.ReplaceOne(client => client.Id == id, clientIn);
            //Get(clientIn.Id);
        }

        public void Remove(Client clientIn) => _clients.DeleteOne(client => client.Id == clientIn.Id);

    }
}
