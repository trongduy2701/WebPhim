using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Areas.Admin.Models
{
    public class MovieListViewModel
    {
        public List<Movies> TopViewMovies { get; set; }
        public List<Movies> TopRateMovies { get; set; }
        public List<Movies> LatestYearMovies { get; set; }
        public List<Movies> TopFavoriteMovies { get; set; }
    }
}