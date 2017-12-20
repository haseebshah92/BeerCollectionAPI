using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeerCollectionAPI.Controllers
{
    public class BeerMasterController : ApiController
    {
        // GET: api/BeerMaster
        [HttpGet]
        public IEnumerable<BeerCollection> GetBeerCollection()
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beers = db.BeerCollections.ToList();
                    if (beers.Count > 0)
                    {
                        return beers;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new List<BeerCollection>();
        }

        [HttpGet]
        // GET: api/BeerMaster/5
        public BeerCollection GetBeerById(int id)
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beers = db.BeerCollections.FirstOrDefault(b => b.ID == id);
                    if (beers != null)
                    {
                        return beers;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new BeerCollection();
        }

        [HttpGet]
        public IEnumerable<BeerCollection> GetBeerByName(string name = "")
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beers = db.BeerCollections.Where(b => b.NAME.Contains(name)).ToList();
                    if (beers != null)
                    {
                        return beers;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new List<BeerCollection>();
        }

        [HttpGet]
        public BeerCollection UpdateBeerCollection(int id, string name, int type, int rating)
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beer = db.BeerCollections.FirstOrDefault(b => b.ID == id);
                    if (beer != null)
                    {
                        beer.NAME = name;
                        beer.TYPE = type;
                        beer.RATING = rating;

                        if (db.SaveChanges() > 0)
                        {
                            return db.BeerCollections.FirstOrDefault(b => b.ID == id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new BeerCollection();
        }
        [HttpGet]
        public BeerCollection UpdateBeerRatingByAverage(int id)
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beer = db.BeerCollections.FirstOrDefault(b => b.ID == id);
                    var average = db.BeerCollections.Select(b => b.RATING).Average();
                    if (beer != null)
                    {
                        beer.RATING = Convert.ToInt32(average);

                        if (db.SaveChanges() > 0)
                        {
                            return db.BeerCollections.FirstOrDefault(b => b.ID == id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new BeerCollection();
        }
        [HttpGet]
        public BeerCollection AddBeer(string name, int type, int rating)
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beer = new BeerCollection()
                    {
                        NAME = name,
                        RATING = rating,
                        TYPE = type
                    };
                    db.BeerCollections.Add(beer);
                    if (db.SaveChanges() > 0)
                    {
                        return beer;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new BeerCollection();
        }
        [HttpGet]
        public string DeleteBeerCollection(int id)
        {
            try
            {
                using (BeerCollectionEntities db = new BeerCollectionEntities())
                {
                    var beer = db.BeerCollections.FirstOrDefault(b => b.ID == id);
                    if (beer != null)
                    {
                        db.BeerCollections.Remove(beer);

                        if (db.SaveChanges() > 0)
                        {
                            return "Beer removed successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "Error in deleting beer.";
        }
    }
}
