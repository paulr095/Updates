using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace Bhasvic10th.iOS
{

	public class AlertCategory
	{

		[PrimaryKey]
		public string Category { get; set; }
		public bool Alert { get; set; }

		public void Switch()
		{
			this.Alert = !this.Alert;
		}

	}
}
