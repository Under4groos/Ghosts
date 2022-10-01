using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010E RID: 270
	public struct InventoryMergeStacksRequest
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x00030FE0 File Offset: 0x0002F1E0
		public InventoryMergeStacksRequest(int originInventoryNetID, int originContainerIndex, int originItemIndex, int targetInventoryNetID, int targetContainerIndex, int targetItemIndex)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginContainerIndex = originContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginItemIndex = originItemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetContainerIndex = targetContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetItemIndex = targetItemIndex;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00031038 File Offset: 0x0002F238
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 6);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginItemIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetItemIndex);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000310E0 File Offset: 0x0002F2E0
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryMergeStacksRequest? Parse(string str)
		{
			InventoryMergeStacksRequest? result;
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
				result = new InventoryMergeStacksRequest?(new InventoryMergeStacksRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), strArray[3].ToInt(0), strArray[4].ToInt(0), strArray[5].ToInt(0)));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003CD RID: 973
		public int OriginInventoryNetID;

		// Token: 0x040003CE RID: 974
		public int OriginContainerIndex;

		// Token: 0x040003CF RID: 975
		public int OriginItemIndex;

		// Token: 0x040003D0 RID: 976
		public int TargetInventoryNetID;

		// Token: 0x040003D1 RID: 977
		public int TargetContainerIndex;

		// Token: 0x040003D2 RID: 978
		public int TargetItemIndex;
	}
}
