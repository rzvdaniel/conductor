using Conductor.Domain.Models;
using System.Collections.Generic;

namespace Conductor.Domain.Interfaces
{
    public interface IFlowService
    {
        Flow GetFlow(string id);
        IEnumerable<Flow> GetFlows(int pageNumber, int pageSize);
        void SaveFlow(Flow flow);
    }
}
