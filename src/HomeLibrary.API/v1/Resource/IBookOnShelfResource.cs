using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IBookOnShelfResource : IResource
    {
        IEnumerable<BookOnShelfDocument> Get(int bookshelfId);
        BookOnShelfDocument Get(int bookshelfId, int bookId);
        void Delete(int bookshelfId, int bookId);
        BookOnShelfDocument Post(int bookshelfId, int bookId);
    }
}