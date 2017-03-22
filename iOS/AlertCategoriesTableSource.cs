using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using System.Globalization;

namespace Bhasvic10th.iOS
{
	public class AlertCategoriesTableSource : UITableViewSource

	{
		public List<AlertCategory> alertCatList;
		string cellidentifier = "AlertCatCell";// defines each cell 

		public AlertCategoriesTableSource(List<AlertCategory> itemList)
		{
			alertCatList = itemList;

		}


		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			
			var cell = tableView.DequeueReusableCell(cellidentifier);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, cellidentifier);
			}
			var alertCat = LocalBhasvicDB.getAlertCategory(alertCatList.ElementAt(indexPath.Row).Category);
			cell.TextLabel.Text = alertCatList.ElementAt(indexPath.Row).Category;
			if (alertCat.Alert == true)
			{
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}
			else
			{
				cell.Accessory = UITableViewCellAccessory.None;
			}
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return alertCatList.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var alertCat = LocalBhasvicDB.getAlertCategory(alertCatList.ElementAt(indexPath.Row).Category);
			if (alertCat.Alert == true)
			{
				alertCat.Switch();
				LocalBhasvicDB.updateAlertCategoryTable(alertCat);
			}
			else
			{
				alertCat.Switch();
				LocalBhasvicDB.updateAlertCategoryTable(alertCat);
			};
			tableView.DeselectRow(indexPath, true);
			tableView.ReloadData();


		}

		//public NewsItem GetItem(int id)
		//{
		//	return alertCatList.ElementAt(id);
		//}
	}
}
