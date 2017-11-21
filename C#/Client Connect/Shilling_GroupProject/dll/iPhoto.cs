using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public interface iPhoto : iRepository<Photo>
    {
        List<Photo> FillPhotos();
        List<Photo> GetSingleClientPhotos(int id);
        List<Photo> GetReleasedPhotos(int id);
        List<Photo> GetUnreleasedPhotos(int id);
        Byte[] GetSinglePhoto(int Id);
    }
}
