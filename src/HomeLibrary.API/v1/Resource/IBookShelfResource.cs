using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IBookShelfResource : IResource
    {
        IEnumerable<BookShelfDocument> Get();
        BookShelfDocument Get(int id);
        BookShelfDocument Update(int id, BookShelfDocument document);
        void Delete(int id);
        BookShelfDocument Post(BookShelfDocument document);
    }
}