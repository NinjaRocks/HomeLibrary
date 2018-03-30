using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IUserResource : IResource
    {
        UserDocument Get(int userId);
        UserDocument Update(int userId, UserDocument document);
        void Delete(int userId);
        UserDocument Post(UserDocument document);
        IEnumerable<UserDocument> Get(string userId, string password);
    }
}