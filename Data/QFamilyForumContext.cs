using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QFamilyForum.Data
{
    public class QFamilyForumContext : DbContext
    {
        public QFamilyForumContext (DbContextOptions<QFamilyForumContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; } = default!;
    }
}
