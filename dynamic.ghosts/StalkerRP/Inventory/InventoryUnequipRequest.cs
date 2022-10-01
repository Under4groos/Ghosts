using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x02000113 RID: 275
	public struct InventoryUnequipRequest
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x00031740 File Offset: 0x0002F940
		public InventoryUnequipRequest(int targetInventoryNetId, int targetContainerIndex, int x, int y, bool rotated, SlotID slotId, bool firstFreeSlot)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetContainerIndex = targetContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotated = rotated;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FirstFreeSlot = firstFreeSlot;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000317A8 File Offset: 0x0002F9A8
		public InventoryUnequipRequest(SlotID slotId)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetContainerIndex = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotated = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FirstFreeSlot = true;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003180C File Offset: 0x0002FA0C
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 7);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.X);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.Y);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(this.Rotated);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.SlotID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(this.FirstFreeSlot);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000318CC File Offset: 0x0002FACC
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryUnequipRequest? Parse(string str)
		{
			InventoryUnequipRequest? result;
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
				result = new InventoryUnequipRequest?(new InventoryUnequipRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), strArray[3].ToInt(0), strArray[4].ToBool(), (SlotID)strArray[5].ToInt(0), strArray[6].ToBool()));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003E4 RID: 996
		public int TargetInventoryNetID;

		// Token: 0x040003E5 RID: 997
		public int TargetContainerIndex;

		// Token: 0x040003E6 RID: 998
		public int X;

		// Token: 0x040003E7 RID: 999
		public int Y;

		// Token: 0x040003E8 RID: 1000
		public bool Rotated;

		// Token: 0x040003E9 RID: 1001
		public SlotID SlotID;

		// Token: 0x040003EA RID: 1002
		public bool FirstFreeSlot;
	}
}
