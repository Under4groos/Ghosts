using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010F RID: 271
	public struct InventoryMoveRequest
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x00031188 File Offset: 0x0002F388
		public InventoryMoveRequest(int originInventoryNetId, int containerIndex, int itemIndex, int targetInventoryNetID, int targetContainerIndex, GridPosition gridPos, bool rotated)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetContainerIndex = targetContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = gridPos.X;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = gridPos.Y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotated = rotated;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00031204 File Offset: 0x0002F404
		public InventoryMoveRequest(int originInventoryNetId, int containerIndex, int itemIndex, int targetInventoryNetID, int targetContainerIndex, int x, int y, bool rotated)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OriginInventoryNetID = originInventoryNetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ContainerIndex = containerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemIndex = itemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetInventoryNetID = targetInventoryNetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetContainerIndex = targetContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotated = rotated;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00031278 File Offset: 0x0002F478
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 8);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.OriginInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.ItemIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetInventoryNetID);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.TargetContainerIndex);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.X);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.Y);
			defaultInterpolatedStringHandler.AppendLiteral(",");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(this.Rotated);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00031354 File Offset: 0x0002F554
		[Description("Given a string, try to convert this into a vector4. The format is \"x,y,z,w\".")]
		public static InventoryMoveRequest? Parse(string str)
		{
			InventoryMoveRequest? result;
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
				result = new InventoryMoveRequest?(new InventoryMoveRequest(strArray[0].ToInt(0), strArray[1].ToInt(0), strArray[2].ToInt(0), strArray[3].ToInt(0), strArray[4].ToInt(0), strArray[5].ToInt(0), strArray[6].ToInt(0), strArray[7].ToBool()));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003D3 RID: 979
		public int OriginInventoryNetID;

		// Token: 0x040003D4 RID: 980
		public int ContainerIndex;

		// Token: 0x040003D5 RID: 981
		public int ItemIndex;

		// Token: 0x040003D6 RID: 982
		public int TargetInventoryNetID;

		// Token: 0x040003D7 RID: 983
		public int TargetContainerIndex;

		// Token: 0x040003D8 RID: 984
		public int X;

		// Token: 0x040003D9 RID: 985
		public int Y;

		// Token: 0x040003DA RID: 986
		public bool Rotated;
	}
}
