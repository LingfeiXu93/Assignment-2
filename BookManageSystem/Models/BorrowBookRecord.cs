
using System;
namespace BookManageSystem.Models
{
	/// <summary>
	/// BorrowBookRecord
	/// </summary>
	public class BorrowBookRecord
	{
		public BorrowBookRecord()
		{}
		#region Model
		private int _id;
		private int? _user_id;
		private string _user_name;
		private int? _book_id;
		private string _book_name;
		private DateTime? _borrowbookdate;
		private DateTime? _returnbookdate;
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
		public int? User_ID
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_Name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Book_ID
		{
			set{ _book_id=value;}
			get{return _book_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Book_Name
		{
			set{ _book_name=value;}
			get{return _book_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BorrowBookDate
		{
			set{ _borrowbookdate=value;}
			get{return _borrowbookdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReturnBookDate
		{
			set{ _returnbookdate=value;}
			get{return _returnbookdate;}
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

