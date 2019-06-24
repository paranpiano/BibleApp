using SQLite;


namespace BibleApp.Models
{
    [Table("BibleVerse")]
    public class BibleVerse
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Version { get; set; }

        [NotNull]
        public string Chapter { get; set; }

        [NotNull]
        public int ChapterNr { get; set; }

        [NotNull]
        public string Verses { get; set; }

    }
}
