using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using System.Linq;
using UIKit;
using System.Text;
using System.IO;
namespace Bhasvic10th.iOS
{
	public static class LocalBhasvicDB
	{
		public static string logTag { get; set; }
		public static string DBLocation { get; set; }
		public static string documentsPath { get; set; }
		public static string libraryPath { get; set; }
		public static List<NewsItem> itemList;
		public static List<NewsItem> categorisedItemList;
		public static SQLite.SQLiteConnection db;

		static LocalBhasvicDB()
		{

			logTag = "LocalBhasvicDB";
			documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			libraryPath = Path.Combine(documentsPath, "../Library/");
			DBLocation = Path.Combine(libraryPath, "NewsItemDB.db3");
			db = new SQLite.SQLiteConnection(DBLocation);
			Console.WriteLine(db);
			Console.WriteLine(DBLocation);



		}

		static public void createNewsItemTable()
		{
			
			db.CreateTable<NewsItem>();
			//return true;
		}

		static public void dropNewsItemTable()
		{
			db.DropTable<NewsItem>();
		}
		static public void createSettingsItemTable()
		{
			db.CreateTable<SystemSettings>();
		}

		static public void dropSettingsItemTable()
		{
			db.DropTable<SystemSettings>();
		}

		static public void createAlertCategoryTable()
		{
			db.CreateTable<AlertCategory>();
		}

		static public void dropAlertCategoryTable()
		{
			db.DropTable<AlertCategory>();
		}


		// Method needed to identify if the Table has ever been created

		static public List<SQLiteConnection.ColumnInfo> getTableInfo(string tableName)
		{
			List<SQLiteConnection.ColumnInfo> l =  db.GetTableInfo(tableName);
			return l;
		}



		// ***** NewsItemTable methods

		static public bool updateDBWithJSON(string jsonString)
		{
			//Console.WriteLine("jsonString: " + jsonString);
			itemList = JsonConvert.DeserializeObject<List<NewsItem>>(jsonString);
			//categorisedItemList = JsonConvert.DeserializeObject<List<NewsItem>>(jsonString);
			//categorisedItemList.AddRange(itemList);
			db.RunInTransaction(() =>
			{
				foreach (var item in itemList)
				{
					db.InsertOrReplace(item);
				}

			});
			return true;
		}

		static public List<NewsItem> getItemList()
		{
			//var query = db.Table<NewsItem>().Select(res => res);
			var query = db.Query<NewsItem>("select * from NewsItem");
			return query.ToList();
		}
		static public List<NewsItem> getCatItemList(string chosenCat)
		{
			var query2 = db.Query<NewsItem>("select * from NewsItem where Category = '" + chosenCat + "'");

			if (chosenCat.Equals("All"))
			{
				query2 = db.Query<NewsItem>("select * from NewsItem");
			}
			return query2.ToList();
		
		}


		static public List<NewsItem> getEventList()
		{
			var query = db.Query<NewsItem>("select * from NewsItem where DateOfEvent IS NOT NULL" );
			return query.ToList();
		}


		// ***** AlertCategoryTable queries

		static public bool updateAlertCategoryTable(AlertCategory alertCategory)
		{
		
			db.RunInTransaction(() =>
			{
					db.InsertOrReplace(alertCategory);
			});
			return true;
		
		}

		static public List<AlertCategory> getAllAlertCategories()
		{
			return db.Query<AlertCategory>("select * from AlertCategory").ToList().OrderBy(x => x.Category).ToList();
		}

		static public AlertCategory getAlertCategory(string category)
		{
			return db.Find<AlertCategory>(category);
		}

	}
}