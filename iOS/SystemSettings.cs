using SQLite;

namespace Bhasvic10th.iOS
{
	public class SystemSettings
	{
		
		public int ID { get; set; }
	//	[ForeignKey(typeof(ChosenCategories))]
	//	public string Category { get; set; }
		public bool Alerts { get; set;}
	//	[ManyToOne]
	//	public ChosenCategories ChosenCategories { get; set;}

	}
}
