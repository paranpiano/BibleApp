﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleApp.Models;

namespace BibleApp.Services
{

    public class TitleDataStore : IDataStore<Item>
    {
        List<Item> items;

        public TitleDataStore()
        {
            items = new List<Item>();
            var chaterItems = new List<Item>
            {
                new Item { Id = "korean_1_Ge", Text ="창세기"         , Description="" },
                new Item { Id = "korean_2_Ex ", Text ="출애굽기"      , Description="" },
                new Item { Id = "korean_3_Le", Text ="레위기"        , Description="" },
                new Item { Id = "korean_4_Nu", Text ="민수기"        , Description="" },
                new Item { Id = "korean_5_De", Text ="신명기"        , Description="" },
                new Item { Id = "korean_6_Jos", Text ="여호수아"     , Description="" },
                new Item { Id = "korean_7_Ju", Text ="사사기"       , Description="" },
                new Item { Id = "korean_8_Ru", Text ="룻기"        , Description="" },
                new Item { Id = "korean_9_1S", Text ="사무엘상"      , Description="" },
                new Item { Id = "korean_10_2S", Text ="사무엘하"      , Description="" },
                new Item { Id = "korean_11_1K", Text ="열왕기상"      , Description="" },
                new Item { Id = "korean_12_2K", Text ="열왕기하"      , Description="" },
                new Item { Id = "korean_13_1Chr", Text ="역대상"       , Description="" },
                new Item { Id = "korean_14_2Chr", Text ="역대하"       , Description="" },
                new Item { Id = "korean_15_Ezr", Text ="에스라"       , Description="" },
                new Item { Id = "korean_16_Ne", Text ="느헤미야"      , Description="" },
                new Item { Id = "korean_17_Est", Text ="에스더"        , Description="" },
                new Item { Id = "korean_18_Jb", Text ="욥기"         , Description="" },
                new Item { Id = "korean_19_Ps", Text ="시편"         , Description="" },
                new Item { Id = "korean_20_Pr", Text ="잠언"         , Description="" },
                new Item { Id = "korean_21_Ec", Text ="전도서"       , Description="" },
                new Item { Id = "korean_22_Song", Text ="아가"        , Description="" },
                new Item { Id = "korean_23_Is", Text ="이사야"        , Description="" },
                new Item { Id = "korean_24_Je", Text ="예레미야"      , Description="" },
                new Item { Id = "korean_25_La", Text ="예레미야애가"  , Description="" },
                new Item { Id = "korean_26_Ez", Text ="에스겔"        , Description="" },
                new Item { Id = "korean_27_Da", Text ="다니엘"        , Description="" },
                new Item { Id = "korean_28_Ho", Text ="호세아"        , Description="" },
                new Item { Id = "korean_29_Joel", Text ="요엘"        , Description="" },
                new Item { Id = "korean_30_Am", Text ="아모스"       , Description="" },
                new Item { Id = "korean_31_Obad", Text ="오바댜"       , Description="" },
                new Item { Id = "korean_32_Jona",Text ="요나"       , Description="" },
                new Item { Id = "korean_33_Mi", Text ="미가"         , Description="" },
                new Item { Id = "korean_34_Na",Text ="나훔"       , Description="" },
                new Item { Id = "korean_35_Ha", Text ="하박국"        , Description="" },
                new Item { Id = "korean_36_Zeph", Text ="스바냐"        , Description="" },
                new Item { Id = "korean_37_Hagg", Text ="학개"         , Description="" },
                new Item { Id = "korean_38_Zech", Text ="스가랴"        , Description="" },
                new Item { Id = "korean_39_Ma", Text ="말라기"        , Description="" },
                new Item { Id = "korean_40_Mt", Text ="마태복음"      , Description="" },
                new Item { Id = "korean_41_Mak", Text ="마가복음"     , Description="" },
                new Item { Id = "korean_42_Lk", Text ="누가복음"     , Description="" },
                new Item { Id = "korean_43_Jn", Text ="요한복음"     , Description="" },
                new Item { Id = "korean_44_Ac", Text ="사도행전"     , Description="" },
                new Item { Id = "korean_45_Ro", Text ="로마서"        , Description="" },
                new Item { Id = "korean_46_1Co", Text ="고린도전서"   , Description="" },
                new Item { Id = "korean_47_2Co", Text ="고린도후서"   , Description="" },
                new Item { Id = "korean_48_Ga", Text ="갈라디아서"    , Description="" },
                new Item { Id = "korean_49_Ep", Text ="에베소서"      , Description="" },
                new Item { Id = "korean_50_Phl", Text ="빌립보서"      , Description="" },
                new Item { Id = "korean_51_Co", Text ="골로새서"      , Description="" },
                new Item { Id = "korean_52_1Th", Text ="데살로니가전서", Description="" },
                new Item { Id = "korean_53_2Th", Text ="데살로니가후서", Description="" },
                new Item { Id = "korean_54_1Ti", Text ="디모데전서"   , Description="" },
                new Item { Id = "korean_55_2Ti", Text ="디모데후서"   , Description="" },
                new Item { Id = "korean_56_Tit", Text ="디도서"      , Description="" },
                new Item { Id = "korean_57_Phm", Text ="빌레몬서"     , Description="" },
                new Item { Id = "korean_58_He", Text ="히브리서"      , Description="" },
                new Item { Id = "korean_59_Ja", Text ="야고보서"      , Description="" },
                new Item { Id = "korean_60_1Pe", Text ="베드로전서"   , Description="" },
                new Item { Id = "korean_61_2Pe", Text ="베드로후서"   , Description="" },
                new Item { Id = "korean_62_1Jhn", Text ="요한1서"       , Description="" },
                new Item { Id = "korean_63_2Jhn", Text ="요한2서"       , Description="" },
                new Item { Id = "korean_64_3Jhn", Text ="요한3서"       , Description="" },
                new Item { Id = "korean_65_Jude", Text ="유다서"       , Description="" },
                new Item { Id = "korean_66_Re", Text ="요한계시록"    , Description="" },
            };



            foreach (var item in chaterItems)
            {
                items.Add(item);
            }
        }

        //don' use
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }
        //don' use
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
        //don' use
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


//English title
//korean__1__Ge Genesis
//korean__2__Ex Exodus
//korean__3__Le Leviticus
//korean__4__Nu Numbers
//korean__5__De Deuteronomy
//korean__6__Jos Joshua
//korean__7__Ju Judges
//korean__8__Ru Ruth
//korean__9__1S	1 Samuel
//korean__10__2S  2 Samuel
//korean__11__1K  1 Kings
//korean__12__2K  2 Kings
//korean__13__1Chr    1 Chronicles
//korean__14__2Chr    2 Chronicles
//korean__15__Ezr Ezra
//korean__16__Ne  Nehemiah
//korean__17__Est Esther
//korean__18__Jb  Job
//korean__19__Ps  Psalms
//korean__20__Pr  Proverbs
//korean__21__Ec  Ecclesiastes
//korean__22__Song    Song of Songs
//korean__23__Is  Isaiah
//korean__24__Je  Jeremiah
//korean__25__La  Lamentations
//korean__26__Ez  Ezekiel
//korean__27__Da  Daniel
//korean__28__Ho  Hosea
//korean__29__Joel    Joel
//korean__30__Am  Amos
//korean__31__Obad    Obadiah
//korean__32__Jona    Jonah
//korean__33__Mi  Micah
//korean__34__Na  Nahum
//korean__35__Ha  Habakkuk
//korean__36__Zeph    Zephaniah
//korean__37__Hagg    Haggai
//korean__38__Zech    Zechariah
//korean__39__Ma  Malachi
//korean__40__Mt  Matthew
//korean__41__Mak Mark
//korean__42__Lk  Luke 1
//korean__43__Jn John
//korean__44__Ac Acts
//korean__45__Ro Romans
//korean__46__1Co	1 Corinthians
//korean__47__2Co 2 Corinthians
//korean__48__Ga  Galatians
//korean__49__Ep  Ephesians
//korean__50__Phl Philippians
//korean__51__Co  Colossians
//korean__52__1Th 1 Thessalonians
//korean__53__2Th 2 Thessalonians
//korean__54__1Ti 1 Timothy
//korean__55__2Ti 2 Timothy
//korean__56__Tit Titus
//korean__57__Phm Philemon
//korean__58__He  Hebrews
//korean__59__Ja  James
//korean__60__1Pe 1 Peter
//korean__61__2Pe 2 Peter
//korean__62__1Jhn    1 John
//korean__63__2Jhn    2 John
//korean__64__3Jhn    3 John
//korean__65__Jude    Jude
//korean__66__Re  Revelation

