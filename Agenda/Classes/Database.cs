using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Agenda.Classes
{
    public static class Database
    {
        static string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string file = "agenda.db";
        static string path = Path.Combine(folder, file);

        public static bool CreateDatabase()
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                connection.CreateTableAsync<Contact>();
                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public static async Task<bool> RegisterContact(Contact data, bool insert)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);

                if (insert)
                    await db.InsertAsync(data);
                else
                    await db.UpdateAsync(data);

                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public static async Task<List<Contact>> GetContacts()
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                return await db.Table<Contact>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public static async Task<Contact> GetContact(int id)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                return await db.Table<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
    }
}