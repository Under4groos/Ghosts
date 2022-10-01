using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory
{
	// Token: 0x02000107 RID: 263
	public class GridPosition
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002FC44 File Offset: 0x0002DE44
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		public int X { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002FC55 File Offset: 0x0002DE55
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0002FC5D File Offset: 0x0002DE5D
		public int Y { get; set; }

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002FC66 File Offset: 0x0002DE66
		public GridPosition(int x, int y)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = y;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002FC86 File Offset: 0x0002DE86
		public GridPosition(GridPosition itemPosition)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.X = itemPosition.X;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Y = itemPosition.Y;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002FCB0 File Offset: 0x0002DEB0
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
			defaultInterpolatedStringHandler.AppendLiteral("X: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.X);
			defaultInterpolatedStringHandler.AppendLiteral(",  Y: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.Y);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002FD00 File Offset: 0x0002DF00
		public bool IsValid()
		{
			return !this.Equals(GridPosition.Invalid);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002FD10 File Offset: 0x0002DF10
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.X);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Y);
					result = stream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002FD88 File Offset: 0x0002DF88
		public static GridPosition Deserialize(BinaryReader reader)
		{
			return new GridPosition(reader.ReadInt32(), reader.ReadInt32());
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002FD9B File Offset: 0x0002DF9B
		private bool Equals(GridPosition other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x040003B2 RID: 946
		public static readonly GridPosition Invalid = new GridPosition(-1, -1);
	}
}
