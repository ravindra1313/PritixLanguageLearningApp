using PritixDictionary.Models;
using PritixDictionary.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Database
{
    public class PritixDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PritixDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Dictionary>().Wait();
            database.CreateTableAsync<DictionaryDetail>().Wait();
            database.CreateTableAsync<UserDictionaryMapping>().Wait();
            database.CreateTableAsync<UserProgress>().Wait();
        }
        public User CurrentLoggedInUser { set; get; }
        public Dictionary CurrentDictionaryInUse { get; set; }

        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public string GetUserNameAsync(string email, string password)
        {
            try
            {
                return database.Table<User>().Where(i => i.Email == email && i.Password == password).FirstOrDefaultAsync().Result.Name;
            }
            catch (Exception e)
            {

                return null;
            }
        }


        public Task<int> SaveUserAsync(User item)
        {
            return database.InsertAsync(item);
        }

        public Task<User> GetUserById(int Id)
        {
            return database.Table<User>().Where(i => i.UserId == Id).FirstOrDefaultAsync();
        }
        public Task<User> GetUserByMailIdAndPassword(string email, string password)
        {
            return database.Table<User>().Where(i => i.Email == email && i.Password == password).FirstOrDefaultAsync();
        }



        public void AddDefaultDictionary()
        {
            try
            {
                Dictionary defaultDictionary = new Dictionary()
                {
                    FromLanguage = AppConstants.DEFAULT_FROM_LANGUAGE,
                    ToLanguage = AppConstants.DEFAULT_TO_LANGUAGE
                };
                
                Dictionary lastSavedDictionary = GetDictionaryByLanguage(defaultDictionary.FromLanguage, defaultDictionary.ToLanguage).Result;
                var dictionaryId = 0;
                if (lastSavedDictionary==null)
                {

                    dictionaryId = SaveDictionaryItemAsync(defaultDictionary).Result;
                }
                else
                {
                    dictionaryId = lastSavedDictionary.DictionaryId;
                }
                
                SaveUserDictionaryMapping(new UserDictionaryMapping() { DictionaryId = dictionaryId, UserId = this.CurrentLoggedInUser.UserId }).Wait();
                var userDictMapping = GetUserDictMappingByDictId(dictionaryId).Result;
                for (int i = 0; i < AppConstants.DEFAULT_DICTIONARY_KEYS.Count; i++)
                {
                    SaveDictionaryDetails(new DictionaryDetail()
                    {
                        MappingKey = userDictMapping.MappingIndex,
                        Key = AppConstants.DEFAULT_DICTIONARY_KEYS[i],
                        Value = AppConstants.DEFAULT_DICTIONARY_VALUES[i]
                    });
                }
            }
            catch (Exception e)
            {

            }
        }

        //Dictionary
        public Task<int> SaveDictionaryItemAsync(Dictionary item)
        {
            return database.InsertAsync(item);
        }
        public Task<List<Dictionary>> GetDictionaries()
        {
            return database.Table<Dictionary>().ToListAsync();
        }

        public Task<Dictionary> GetDictionaryByLanguage(string FromLanguage, string ToLanguage)
        {
            return database.Table<Dictionary>().Where(x => x.FromLanguage == FromLanguage && x.ToLanguage == ToLanguage).FirstOrDefaultAsync();
        }
        public Task<Dictionary> GetDictionaryById(int dictId)
        {
            return database.Table<Dictionary>().Where(x => x.DictionaryId == dictId).FirstOrDefaultAsync();
        }







        //User Mapping
        public Task<int> SaveUserDictionaryMapping(UserDictionaryMapping userDictMapping)
        {
            return database.InsertAsync(userDictMapping);
        }
        public Task<List<UserDictionaryMapping>> GetAllDictionariesByUserId(int userId)
        {
            return database.Table<UserDictionaryMapping>().Where(i => i.UserId == userId).ToListAsync();
        }
        public Task<UserDictionaryMapping> GetUserDictMappingByDictId(int dictId)
        {
            return database.Table<UserDictionaryMapping>().Where(x => x.UserId == this.CurrentLoggedInUser.UserId && x.DictionaryId == dictId).FirstOrDefaultAsync();
        }
       

        //Dictionary Details
        public Task<int> SaveDictionaryDetails(DictionaryDetail dictDetail)
        {
            return database.InsertAsync(dictDetail);
        }
        public Task<int> SaveAllDictionaryDetails(IEnumerable<DictionaryDetail> dictDetails)
        {
            return database.InsertAllAsync(dictDetails);
        }
        public Task<List<DictionaryDetail>> GetDictionaryDetailsByMappingIndex(int mappingIndex)
        {
            return database.Table<DictionaryDetail>().Where(x => x.MappingKey == mappingIndex).ToListAsync();
        }
        public Task<int> DeleteDetailsByMappingIndex(int mappingIndex)
        {
            return database.ExecuteAsync("Delete from DictionaryDetail where MappingKey = " + mappingIndex);            
        }

        //UserProgress
        public Task<List<UserProgress>> GetCurrentUserProgress()
        {
           return database.Table<UserProgress>().Where(x => x.UserId == CurrentLoggedInUser.UserId).ToListAsync();
        }
         public Task<UserProgress> GetUserProgressForCurrentDictionary()
        {
            return database.Table<UserProgress>().Where(x => x.UserId == CurrentLoggedInUser.UserId && x.DictionaryId == CurrentDictionaryInUse.DictionaryId).FirstOrDefaultAsync();
        }
        public Task<UserProgress> GetUserLatestProgress()
        {
            return database.Table<UserProgress>().Where(x => x.UserId == CurrentLoggedInUser.UserId).OrderByDescending(x=>x.ScoreTime).FirstOrDefaultAsync();
        }
        public Task<int> SaveUserProgress(UserProgress progress)
        {
            return database.InsertOrReplaceAsync(progress);
        }
    }
}
