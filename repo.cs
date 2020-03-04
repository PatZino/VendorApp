using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDbSpCallMVC
{
	public static class repo
	{
		//Get page number

		//public static int GetPages<TInput>(IQueryable<TInput> query, int pageSize) where TInput : class
		public static int GetPages<TInput>(IQueryable<TInput> query, int pageSize) where TInput : class
		{
			int numberOfPages = query.Count() / pageSize;
			int remainder = query.Count() % pageSize;
			if (remainder > 0)
			{
				numberOfPages++;
			}
			return numberOfPages;
		}
	}
}
