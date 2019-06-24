using System;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using BibleApp.Models;

namespace BibleApp.DatabaseAccess
{
    public class DataContext
    {
        //member variables
        private SQLiteConnection database;
        public static object collisionLock = new object();
        public List<BibleVerse> BibleVerses { get; set; } = new List<BibleVerse>();

        //constructor
        public DataContext()
        {
            

            database =
                DependencyService.Get<IDatabaseConnection>().DbConnection();

            var tableInfo = GetAllTables();

            if (!tableInfo.Where(t => t.name == "BibleVerse").Any())
            {
                database.CreateTable<BibleVerse>();
            }
           
        }

        //DB CRUD operation
        public void SaveAllBibleVerses()
        {
            lock (collisionLock)
            {
                foreach (var verse in this.BibleVerses)
                {
                    if (verse.Id != 0)
                    {
                        database.Update(verse);
                    }
                    else
                    {
                        database.Insert(verse);
                    }
                }
            }
        }
        public IEnumerable<BibleVerse> GetFilteredVerses(string chapter , int chapterNr , int verseNr)
        {
            lock (collisionLock)
            {

                if (chapterNr!=0 && verseNr !=0)
                {
                    //
                    var query = from verse in database.Table<BibleVerse>()
                                where verse.Chapter == chapter
                                select verse;

                    //verser 만 골라서 가져올 수 있도록 업데이트 필요
                    return query.AsEnumerable();
                }
                else if (chapterNr != 0)
                {
                    //
                    var query = from verse in database.Table<BibleVerse>()
                                where verse.Chapter == chapter && verse.ChapterNr == chapterNr
                                select verse;

                    //verser 만 골라서 가져올 수 있도록 업데이트 필요
                    return query.AsEnumerable();
                }
                else
                {
                    var query = from verse in database.Table<BibleVerse>()
                                where verse.Chapter == chapter && verse.ChapterNr == 1
                                select verse;

                    //verser 만 골라서 가져올 수 있도록 업데이트 필요
                    return query.AsEnumerable();
                }
            }

        }
        public void DeleteAllVerses()
        {
            lock (collisionLock)
            {
                database.DropTable<BibleVerse>();
                database.CreateTable<BibleVerse>();
            }
        }
        public List<TableName> GetAllTables()
        {
            string queryString = $"SELECT name FROM sqlite_master WHERE type = 'table'";
            return database.Query<TableName>(queryString);
        }
        public int GetDataRawCount()
        {
            lock (collisionLock)
            {

                var query = from verse in database.Table<BibleVerse>()
                            select verse;
                return query.AsEnumerable().ToList().Count;
            }
        }

        //Memory Data Function
        public void AddBibleVerse(BibleVerse bibleVerse)
        {
            this.BibleVerses.Add(bibleVerse);
        }
        private void AddMockBibleVerse()
        {
            //this.BibleVerses.Add(new BibleVerse
            //{
            //      Version = "Korean",
            //      Chapter = "Ge",
            //      VerseNr = 1,
            //      Verse = "James, a servant of God and of the Lord Jesus Christ, to the twelve tribes which are scattered abroad, greeting."
            //});
        }
    }


}
