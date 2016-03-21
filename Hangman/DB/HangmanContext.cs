using System.Data.Entity;

namespace Hangman.DB
{
    class HangmanContext : DbContext
    {
        static HangmanContext()
        {
            Database.SetInitializer(new HangmanContextInitializer());
        }

        public HangmanContext() : base("DefaultConnection")
            { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}
