using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IBorrowerResource : IResource
    {
        IEnumerable<BorrowerDocument> Get();
        BorrowerDocument Get(int id);
        BorrowerDocument Update(int id, BorrowerDocument document);
        void Delete(int id);
        BorrowerDocument Post(BorrowerDocument document);
    }
}