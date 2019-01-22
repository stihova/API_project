using BookLibrary.Business;
using BookLibrary.Entities;
using BookLibrary.Rpc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLibrary.Rpc.Controllers
{
    public class ReaderController : ApiController
    {
        [HttpGet]
        //[Route("api/reader/GetAllReaders")]
        public List<ReaderModel> GetAllReaders()
        {
            ReaderService readersService = new ReaderService();
            var allReaders = readersService.GetAll()
                .Select(c => new ReaderModel(c))
                .ToList();
            return allReaders;
        }

        [HttpGet]
        //[Route("api/reader/GetReaderByID?readerID=1")]
        public IHttpActionResult GetReaderByID(int? readerID)
        {
            if (readerID == null)
                return BadRequest("the parameter readerID is empty");

            ReaderService readersService = new ReaderService();
            Reader reader = readersService.GetReaderByID(readerID.Value);
            if (reader == null)
                return BadRequest($"Could not find reader with ID: {readerID}");

            ReaderModel apiReader = new ReaderModel(reader);
            return Ok(apiReader);
        }

        [HttpPost]
        public IHttpActionResult Edit(ReaderModel reader)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbBook = readerService.GetReaderByID(reader.ID);
                if (dbBook == null)
                    return NotFound();


                reader.CopyValuesToEntity(dbBook);
                readerService.EditReader(dbBook);

                return StatusCode(HttpStatusCode.NoContent); // or use Ok()
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(ReaderModel reader)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbBook = new Reader();

                reader.CopyValuesToEntity(dbBook);
                readerService.AddReader(dbBook);

                // return the newly created Book
                return Ok(dbBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(int readerID)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbBook = readerService.GetReaderByID(readerID);
                if (dbBook == null)
                    return NotFound();

                readerService.DeleteReader(readerID);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}