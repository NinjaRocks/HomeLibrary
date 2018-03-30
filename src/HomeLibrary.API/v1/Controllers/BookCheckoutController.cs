using System.Collections.Generic;
using System.Web.Http;
using HomeLibrary.API.v1.Contracts;
using MicroFx.Http;

namespace HomeLibrary.API.v1.Controllers
{
    [RoutePrefix("v1/checkouts/bookshelves")]
    public class BookCheckoutController : ApiController
    {
        private readonly IBookCheckoutResource resource;

        public BookCheckoutController(IBookCheckoutResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookshelfId}/books")]
        public IHttpContentResult<IEnumerable<BookCheckoutDocument>> Get(int bookshelfId)
        {
            return Request.CreateContentResponse(resource.Get(bookshelfId));
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookshelfId}/books/{bookId}")]
        public IHttpContentResult<BookCheckoutDocument> Get(int bookshelfId, int bookId)
        {
            return Request.CreateContentResponse(resource.Get(bookshelfId, bookId));
        }

        
        [AllowAnonymous]
        [HttpPut, Route("{bookshelfId}/books/{bookId}/borrowers/{borrowerId}")]
        public IHttpContentResult<BookCheckoutDocument> Put(int bookshelfId, int bookId, int borrowerId, BookCheckoutDocument document)
        {
            return Request.CreateContentResponse(resource.Put(bookshelfId, bookId, borrowerId, document));
        }
    }
}