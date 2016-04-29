
using System;
namespace BookManageSystem.Models
{
	/// <summary>
	/// Books:
	/// </summary>
	public class Books
	{
		public Books()
		{}
		#region Model
		private int _id;
		private string _bookname;
		private string _states;
		private DateTime? _createdate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BookName
		{
			set{ _bookname=value;}
			get{return _bookname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string States
		{
			set{ _states=value;}
			get{return _states;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

