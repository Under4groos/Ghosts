using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010C RID: 268
	public struct InventoryDropRequest
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x00030CCA File Offset: 0x0002EECA
		public InventoryDropRequest(int originInventoryNetId, int containerIndex, int itemIndex, int count, SlotID slotId)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Count = count;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00030D0A File Offset: 0x0002EF0A
		public InventoryDropRequest(SlotID slotId)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Count = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00030D48 File Offset: 0x0002EF48
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ItemIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.Count);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.SlotID);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00030DD8 File Offset: 0x0002EFD8
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryDropRequest? Parse(string str)
		{
			InventoryDropRequest? result;
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
				result = new InventoryDropRequest?(new InventoryDropRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), strArray[3].ToInt(0), (SlotID)strArray[4].ToInt(0)));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003C3 RID: 963
		public int OriginInventoryNetID;

		// Token: 0x040003C4 RID: 964
		public int ContainerIndex;

		// Token: 0x040003C5 RID: 965
		public int ItemIndex;

		// Token: 0x040003C6 RID: 966
		public int Count;

		// Token: 0x040003C7 RID: 967
		public SlotID SlotID;
	}
}
