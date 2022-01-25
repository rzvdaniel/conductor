using Conductor.Domain.Interfaces;
using Conductor.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Conductor.Storage.Services
{
    public class FlowRepository : IFlowRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Flow> _collection => _database.GetCollection<Flow>("Flows");

        static FlowRepository()
        {
        }

        public FlowRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public Flow Find(string flowId)
        {
            var result = _collection.Find(x => x.Id == flowId);

            if (!result.Any())
                return null;

            var flow = result.First();
           
            return flow;
        }

        public IEnumerable<Flow> Get(int pageNumber, int pageSize)
        {
            var paginationValid = pageNumber > 0 && pageSize > 0;

            var results = paginationValid ?

                _collection
                    .AsQueryable()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize) :

                _collection
                    .AsQueryable();

            return results;
        }

        public void Save(Flow flow)
        {
            if (_collection.AsQueryable().Any(x => x.Id == flow.Id))
            {
                _collection.FindOneAndReplace(x => x.Id == flow.Id, flow);
                return;
            }

            _collection.InsertOne(flow);
        }
    }
}
