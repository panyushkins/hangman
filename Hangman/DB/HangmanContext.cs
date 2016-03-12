using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DB
{
    class HangmanContext : DbContext
    {
        public HangmanContext() : base("DefaultConnection")
            { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}
