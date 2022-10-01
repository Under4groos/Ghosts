using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010D RID: 269
	public struct InventoryEquipRequest
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x00030E74 File Offset: 0x0002F074
		public InventoryEquipRequest(int originInventoryNetId, int containerIndex, int itemIndex, SlotID slotId, bool firstValidSlot)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FirstValidSlot = firstValidSlot;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00030EB4 File Offset: 0x0002F0B4
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ItemIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.SlotID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(this.FirstValidSlot);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00030F44 File Offset: 0x0002F144
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryEquipRequest? Parse(string str)
		{
			InventoryEquipRequest? result;
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
				result = new InventoryEquipRequest?(new InventoryEquipRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), (SlotID)strArray[3].ToInt(0), strArray[4].ToBool()));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003C8 RID: 968
		public int OriginInventoryNetID;

		// Token: 0x040003C9 RID: 969
		public int ContainerIndex;

		// Token: 0x040003CA RID: 970
		public int ItemIndex;

		// Token: 0x040003CB RID: 971
		public SlotID SlotID;

		// Token: 0x040003CC RID: 972
		public bool FirstValidSlot;
	}
}
