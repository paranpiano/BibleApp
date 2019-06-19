using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleApp.Models;

namespace BibleApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = "1", Text ="창세기"         , Description="" },
                new Item { Id = "2", Text ="출애굽기"      , Description="" },
                new Item { Id = "3", Text ="레위기"        , Description="" },
                new Item { Id = "4", Text ="민수기"        , Description="" },
                new Item { Id = "5", Text ="신명기"        , Description="" },
                new Item { Id = "6", Text ="여호수아"     , Description="" },
                new Item { Id = "7", Text ="사사기"       , Description="" },
                new Item { Id = "8", Text ="룻기"        , Description="" },
                new Item { Id = "9", Text ="사무엘상"      , Description="" },
                new Item { Id = "10", Text ="사무엘하"      , Description="" },
                new Item { Id = "11", Text ="열왕기상"      , Description="" },
                new Item { Id = "12", Text ="열왕기하"      , Description="" },
                new Item { Id = "13", Text ="역대상"       , Description="" },
                new Item { Id = "14", Text ="역대하"       , Description="" },
                new Item { Id = "15", Text ="에스라"       , Description="" },
                new Item { Id = "16", Text ="느헤미야"      , Description="" },
                new Item { Id = "17", Text ="에스더"        , Description="" },
                new Item { Id = "18", Text ="욥기"         , Description="" },
                new Item { Id = "19", Text ="시편"         , Description="" },
                new Item { Id = "20", Text ="잠언"         , Description="" },
                new Item { Id = "21", Text ="전도서"       , Description="" },
                new Item { Id = "22", Text ="아가"        , Description="" },
                new Item { Id = "23", Text ="이사야"        , Description="" },
                new Item { Id = "24", Text ="예레미야"      , Description="" },
                new Item { Id = "25", Text ="예레미야애가"  , Description="" },
                new Item { Id = "26", Text ="에스겔"        , Description="" },
                new Item { Id = "27", Text ="다니엘"        , Description="" },
                new Item { Id = "28", Text ="호세아"        , Description="" },
                new Item { Id = "29", Text ="요엘"        , Description="" },
                new Item { Id = "30", Text ="아모스"       , Description="" },
                new Item { Id = "31", Text ="오바댜"       , Description="" },
                new Item { Id = "32",Text ="요나"       , Description="" },
                new Item { Id = "33", Text ="미가"         , Description="" },
                new Item { Id = "34",Text ="나훔"       , Description="" },
                new Item { Id = "35", Text ="하박국"        , Description="" },
                new Item { Id = "36", Text ="스바냐"        , Description="" },
                new Item { Id = "37", Text ="학개"         , Description="" },
                new Item { Id = "38", Text ="스가랴"        , Description="" },
                new Item { Id = "39", Text ="말라기"        , Description="" },
                new Item { Id = "40", Text ="마태복음"      , Description="" },
                new Item { Id = "41", Text ="마가복음"     , Description="" },
                new Item { Id = "42", Text ="누가복음"     , Description="" },
                new Item { Id = "43", Text ="요한복음"     , Description="" },
                new Item { Id = "44", Text ="사도행전"     , Description="" },
                new Item { Id = "45", Text ="로마서"        , Description="" },
                new Item { Id = "46", Text ="고린도전서"   , Description="" },
                new Item { Id = "47", Text ="고린도후서"   , Description="" },
                new Item { Id = "48", Text ="갈라디아서"    , Description="" },
                new Item { Id = "49", Text ="에베소서"      , Description="" },
                new Item { Id = "50", Text ="빌립보서"      , Description="" },
                new Item { Id = "51", Text ="골로새서"      , Description="" },
                new Item { Id = "52", Text ="데살로니가전서", Description="" },
                new Item { Id = "53", Text ="데살로니가후서", Description="" },
                new Item { Id = "54", Text ="디모데전서"   , Description="" },
                new Item { Id = "55", Text ="디모데후서"   , Description="" },
                new Item { Id = "56", Text ="디도서"      , Description="" },
                new Item { Id = "57", Text ="빌레몬서"     , Description="" },
                new Item { Id = "58", Text ="히브리서"      , Description="" },
                new Item { Id = "59", Text ="야고보서"      , Description="" },
                new Item { Id = "60", Text ="베드로전서"   , Description="" },
                new Item { Id = "61", Text ="베드로후서"   , Description="" },
                new Item { Id = "62", Text ="요한1서"       , Description="" },
                new Item { Id = "63", Text ="요한2서"       , Description="" },
                new Item { Id = "64", Text ="요한3서"       , Description="" },
                new Item { Id = "65", Text ="유다서"       , Description="" },
                new Item { Id = "66", Text ="요한계시록"    , Description="" },
            };                   



            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

