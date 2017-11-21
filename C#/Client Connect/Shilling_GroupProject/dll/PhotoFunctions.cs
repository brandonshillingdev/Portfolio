using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public class PhotoFunctions : RepositoryClass<Photo>, iPhoto
    {
        public PhotoFunctions(DbContext dbcontext) : base(dbcontext)
        {
        }

        public List<Photo> FillPhotos()
        {
            //retuns all photos
            return GetALL().ToList();
        }

        public List<Photo> GetReleasedPhotos(int id)
        {
            //returns a list of all released photos for current client 
            return GetALL().Where(p => p.Client == id && p.Released == 1).ToList();
        }


        public List<Photo> GetSingleClientPhotos(int id)
        {
            //returns all photos where the clientid and the id passed in matches
            return GetALL().Where(c => c.Client == id).ToList();
        }

        public byte[] GetSinglePhoto(int Id)
        {
            //returns the image as a byte array
            return GetALL().SingleOrDefault(i => i.Id == Id).Photo1;
        }

        public List<Photo> GetUnreleasedPhotos(int id)
        {
            //returns a list of all released photos for current client 
            return GetALL().Where(p => p.Client == id && p.Released == 0).ToList();
        }
    }
}
