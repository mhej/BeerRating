using BeerRatingApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace BeerRatingApi.Controllers
{
    [Authorize]
    public class BeersController : ApiController
    {
        ApplicationDbContext beersDbContext = new ApplicationDbContext();
        // GET: api/Beers
        [AllowAnonymous]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        //[Route("beers")]

        public IHttpActionResult Get(string name="", string style="", int pageNumber = 1, int pageSize = 10, string sort = "asc")
        {
            IQueryable<Beer> beers = beersDbContext.Beers;

            if (name!="")
            {
                beers = beers.Where(b => b.Name.Contains(name));
            }

            if (style!="")
            {
                beers = beers.Where(b => b.Style.Contains(style));
            }                       

            switch (sort)
            {
                case "asc":
                    beers = beers.OrderBy(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    //beers = beers.OrderBy(b => b.Name);
                    break;
                case "desc":
                    beers = beers.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
            }
            return Ok(beers);
        }

        // GET: api/Beers/5
        public IHttpActionResult Get(int id)
        {
            var beer = beersDbContext.Beers.Find(id);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }

        // POST: api/Beers
        public IHttpActionResult Post([FromBody]Beer beer)
        {
            string userId = User.Identity.GetUserId();
            beer.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            beersDbContext.Beers.Add(beer);
            beersDbContext.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/Beers/5
        public IHttpActionResult Put(int id, [FromBody]Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();

            var entity = beersDbContext.Beers.FirstOrDefault(b => b.Id == id);

            if (entity == null)
            {
                return BadRequest("No record found against this id");
            }

            if (userId != entity.UserId)
            {
                return BadRequest("You don't have right to update this record...");
            }

            entity.Name = beer.Name;
            entity.Style = beer.Style;
            entity.Brewery = beer.Brewery;
            entity.Country = beer.Country;
            entity.Description = beer.Description;
            entity.Rating = beer.Rating;

            beersDbContext.SaveChanges();
            return Ok("Record updated successfully...");
        }

        // DELETE: api/Beers/5
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            var beer = beersDbContext.Beers.Find(id);
            if (beer == null)
            {
                return BadRequest("No record found against this id");
            }

            if (userId != beer.UserId)
            {
                return BadRequest("You don't have any rights to delete this record...");
            }

            beersDbContext.Beers.Remove(beer);
            beersDbContext.SaveChanges();
            return Ok("Beer deleted");
        }
    }
}

