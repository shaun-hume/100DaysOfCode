using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;
using XamarinForms;
using static XamarinForms.Logs.Logs;

public class BabyTimeLocalDatabase
{
    static SQLiteAsyncConnection Database;

    public static readonly AsyncLazy<BabyTimeLocalDatabase> Instance = new AsyncLazy<BabyTimeLocalDatabase>(async () =>
    {
        var instance = new BabyTimeLocalDatabase();
        await Database.CreateTableAsync<MilkLog>();
        await Database.CreateTableAsync<SleepLog>();
        await Database.CreateTableAsync<ExerciseLog>();
        await Database.CreateTableAsync<PooLog>();
        return instance;
    });

    public BabyTimeLocalDatabase()
    {
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    }

    public Task<int> SaveItemAsync(object item)
    {
        return Database.InsertAsync(item);
    }

    public Task<List<MilkLog>> GetItemsAsync()
    {
        return Database.Table<MilkLog>().ToListAsync();
    }

    public Task DeleteSynchronisedItems()
    {
        return Database.Table<MilkLog>().DeleteAsync(x => true);
    }
}

public class AsyncLazy<T>
{
    readonly Lazy<Task<T>> instance;

    public AsyncLazy(Func<T> factory)
    {
        instance = new Lazy<Task<T>>(() => Task.Run(factory));
    }

    public AsyncLazy(Func<Task<T>> factory)
    {
        instance = new Lazy<Task<T>>(() => Task.Run(factory));
    }

    public TaskAwaiter<T> GetAwaiter()
    {
        return instance.Value.GetAwaiter();
    }
}
