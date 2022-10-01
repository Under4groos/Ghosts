using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x02000111 RID: 273
	public struct InventoryStoreFromGearSlotRequest
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0003151C File Offset: 0x0002F71C
		public InventoryStoreFromGearSlotRequest(SlotID slotID, int targetInventoryNetID)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetID;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00031538 File Offset: 0x0002F738
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.SlotID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetInventoryNetID);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003157C File Offset: 0x0002F77C
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryStoreFromGearSlotRequest? Parse(string str)
		{
			InventoryStoreFromGearSlotRequest? result;
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
				result = new InventoryStoreFromGearSlotRequest?(new InventoryStoreFromGearSlotRequest((SlotID)strArray[0].ToInt(0), strArray[1].ToInt(0)));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003DE RID: 990
		public SlotID SlotID;

		// Token: 0x040003DF RID: 991
		public int TargetInventoryNetID;
	}
}
