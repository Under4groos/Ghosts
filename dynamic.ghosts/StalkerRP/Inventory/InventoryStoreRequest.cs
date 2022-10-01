using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000112 RID: 274
	public struct InventoryStoreRequest
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x00031600 File Offset: 0x0002F800
		public InventoryStoreRequest(int originInventoryNetId, int containerIndex, int itemIndex, int targetInventoryNetID)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetID;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00031634 File Offset: 0x0002F834
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ItemIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetInventoryNetID);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000316AC File Offset: 0x0002F8AC
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryStoreRequest? Parse(string str)
		{
			InventoryStoreRequest? result;
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
				result = new InventoryStoreRequest?(new InventoryStoreRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), strArray[3].ToInt(0)));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003E0 RID: 992
		public int OriginInventoryNetID;

		// Token: 0x040003E1 RID: 993
		public int ContainerIndex;

		// Token: 0x040003E2 RID: 994
		public int ItemIndex;

		// Token: 0x040003E3 RID: 995
		public int TargetInventoryNetID;
	}
}
