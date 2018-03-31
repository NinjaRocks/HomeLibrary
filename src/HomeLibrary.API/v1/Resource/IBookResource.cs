using System.Collections.Generic;
using HomeLibrary.API.v1.Contracts;
using MicroFx;

namespace HomeLibrary.API.v1
{
    public interface IBookResource: IResource
    {
        IEnumerable<BookDocument> Get();
        BookDocument Get(int bookId);
        BookDocument Update(int bookId, BookDocument document);
        void Delete(int bookId);
        BookDocument Create(BookDocument document);
    }
}