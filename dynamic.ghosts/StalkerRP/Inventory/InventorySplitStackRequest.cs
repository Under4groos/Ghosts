using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000110 RID: 272
	public struct InventorySplitStackRequest
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0003140C File Offset: 0x0002F60C
		public InventorySplitStackRequest(int originInventoryNetId, int containerIndex, int itemIndex)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00031434 File Offset: 0x0002F634
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ItemIndex);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00031490 File Offset: 0x0002F690
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventorySplitStackRequest? Parse(string str)
		{
			InventorySplitStackRequest? result;
			try
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				str = str.Trim(new char[]
				{
					'[',
					']',
					' ',
					'\n',
					'\r',
					'\t',
					'"'
				});
				string[] strArray = str.Split(new char[]
				{
					' ',
					',',
					';',
					'\n',
					'\r'
				}, StringSplitOptions.RemoveEmptyEntries);
				result = new InventorySplitStackRequest?(new InventorySplitStackRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0)));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003DB RID: 987
		public int OriginInventoryNetID;

		// Token: 0x040003DC RID: 988
		public int ContainerIndex;

		// Token: 0x040003DD RID: 989
		public int ItemIndex;
	}
}
