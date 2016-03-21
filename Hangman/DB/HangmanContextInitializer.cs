using System.Data.Entity;

namespace Hangman.DB
{
    class HangmanContextInitializer : CreateDatabaseIfNotExists<HangmanContext>
    {
        protected override void Seed(HangmanContext db)
        {
            Category c1 = new Category { Name = "Города" };
            Word w1 = new Word { Text = "Москва", Category = c1 };
            Word w2 = new Word { Text = "Курск", Category = c1 };
            Word w3 = new Word { Text = "Белгород", Category = c1 };
            var listCat1 = new[] { w1, w2, w3 };
            Category c2 = new Category { Name = "Фрукты" };
            Word w4 = new Word { Text = "Яблоко", Category = c2 };
            Word w5 = new Word { Text = "Банан", Category = c2 };
            Word w6 = new Word { Text = "Черешня", Category = c2 };
            var listCat2 = new[] {w4, w5, w6 };

            db.Categories.Add(c1);
            db.Words.AddRange(listCat1);
            db.Categories.Add(c2);
            db.Words.AddRange(listCat2);

            db.SaveChanges();
        }
    }
}
