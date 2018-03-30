using System;
using System.Collections.Generic;
using System.Web.Http;
using HomeLibrary.API.v1.Contracts;
using MicroFx.Http;

namespace HomeLibrary.API.v1.Controllers
{
    [RoutePrefix("v1/bookshelves")]
    public class BookShelfController : ApiController
    {
        private readonly IBookShelfResource resource;

        public BookShelfController(IBookShelfResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public IHttpContentResult<IEnumerable<BookShelfDocument>> Get()
        {
            return Request.CreateContentResponse(resource.Get());
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookshelfId}")]
        public IHttpContentResult<BookShelfDocument> Get(int bookshelfId)
        {
            return Request.CreateContentResponse(resource.Get(bookshelfId));
        }

        [AllowAnonymous]
        [HttpPost, Route]
        public IHttpContentResult<BookShelfDocument> Post(BookShelfDocument document)
        {
            var result = resource.Post(document);
            return Request.CreateNewContentResponse(new Uri($"v1/bookshelves/{result.BookShelfId}"), result);
        }

        [AllowAnonymous]
        [HttpPut, Route("{bookshelfId}")]
        public IHttpContentResult<BookShelfDocument> Put(int bookshelfId, BookShelfDocument document)
        {
            return Request.CreateContentResponse(resource.Update(bookshelfId, document));
        }

        [AllowAnonymous]
        [HttpDelete, Route("{bookshelfId}")]
        public IHttpActionResult Delete(int bookshelfId)
        {
            resource.Delete(bookshelfId);
            return Request.CreateNoContentResponse();
        }
    }
}