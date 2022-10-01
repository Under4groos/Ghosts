using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200014C RID: 332
	public static class IListExtension
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x0003D5B8 File Offset: 0x0003B7B8
		public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
		{
			int count = 0;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (match(list[i]))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					count++;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					list.RemoveAt(i);
				}
			}
			return count;
		}
	}
}
