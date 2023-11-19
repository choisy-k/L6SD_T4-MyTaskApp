//using SQLite;

//namespace MauiApp1.Database
//{
//    //The TodoItemDatabase uses asynchronous lazy initialization to delay initialization of the database until it's first accessed, with a simple Init method that gets called by each method in the class:
//    public class TodoItemDatabase
//    {
//        SQLiteAsyncConnection Database;

//        public TodoItemDatabase()
//        {
//        }

//        async Task Init()
//        {
//            if (Database is not null)
//                return;

//            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
//            var result = await Database.CreateTableAsync<TodoItem>();
//        }

//        // The TodoItemDatabase class includes methods for the four types of data manipulation: create, read, edit, and delete. The SQLite.NET library provides a simple Object Relational Map (ORM) that allows you to store and retrieve objects without writing SQL statements.
//        public async Task<List<TodoItem>> GetItemsAsync()
//        {
//            await Init();
//            return await Database.Table<TodoItem>().ToListAsync();
//        }

//        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
//        {
//            await Init();
//            return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

//            // SQL queries are also possible
//            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
//        }

//        public async Task<TodoItem> GetItemAsync(int id)
//        {
//            await Init();
//            return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
//        }

//        public async Task<int> SaveItemAsync(TodoItem item)
//        {
//            await Init();
//            if (item.ID != 0)
//                return await Database.UpdateAsync(item);
//            else
//                return await Database.InsertAsync(item);
//        }

//        public async Task<int> DeleteItemAsync(TodoItem item)
//        {
//            await Init();
//            return await Database.DeleteAsync(item);
//        }
//    }
//}
