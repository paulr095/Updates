using Foundation;
using System;
using UIKit;

namespace Bhasvic10th.iOS
{
    public partial class SettingsCategoryPicker : UITableViewController
    {
        public SettingsCategoryPicker (IntPtr handle) : base (handle)
        {
        }

		public override void ViewWillAppear(bool animated)
		{
			TableView.Source = new AlertCategoriesTableSource(LocalBhasvicDB.getAllAlertCategories());
			base.ViewWillAppear(animated);
		}
	}
}