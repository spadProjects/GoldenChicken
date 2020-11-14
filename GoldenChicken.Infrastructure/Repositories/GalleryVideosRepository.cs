using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenChicken.Core.Models;

namespace GoldenChicken.Infrastructure.Repositories
{
    public class GalleryVideosRepository : BaseRepository<GalleryVideo, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public GalleryVideosRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
