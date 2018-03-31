using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IBookCheckoutResource : IResource
    {
        IEnumerable<BookCheckoutDocument> Get(int bookshelfId);
        BookCheckoutDocument Get(int bookshelfId, int bookId);
        BookCheckoutDocument Put(int bookshelfId, int bookId, int borrowerId, BookCheckoutDocument document);
    }
}