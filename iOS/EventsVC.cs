using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace Bhasvic10th.iOS
{
    public partial class EventsVC : UITableViewController
    {
        public EventsVC (IntPtr handle) : base (handle)

        {
        }
		public override void ViewWillAppear(bool animated)
		{
			List<NewsItem> eventItems = LocalBhasvicDB.getEventList();
			TableView.Source = new EventsTableSource(eventItems);


			base.ViewWillAppear(animated);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "EventsSegue")
			{ // set in Storyboard
				var navctlr = segue.DestinationViewController as EventsItemDetailedVCs;
				if (navctlr != null)
				{
					var source = TableView.Source as EventsTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask(this, item);
				
				}
			}
		
		}

    }
}