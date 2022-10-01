using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory
{
	// Token: 0x02000108 RID: 264
	public class GridSize
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002FDC9 File Offset: 0x0002DFC9
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0002FDD1 File Offset: 0x0002DFD1
		public int Width { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002FDDA File Offset: 0x0002DFDA
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0002FDE2 File Offset: 0x0002DFE2
		public int Height { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002FDEB File Offset: 0x0002DFEB
		public int EffectiveWidth
		{
			get
			{
				if (!this.IsRotated)
				{
					return this.Width;
				}
				return this.Height;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002FE02 File Offset: 0x0002E002
		public int EffectiveHeight
		{
			get
			{
				if (!this.IsRotated)
				{
					return this.Height;
				}
				return this.Width;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002FE19 File Offset: 0x0002E019
		public int Total
		{
			get
			{
				return this.Width * this.Height;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002FE28 File Offset: 0x0002E028
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0002FE30 File Offset: 0x0002E030
		public bool IsRotated { get; set; }

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002FE39 File Offset: 0x0002E039
		public GridSize(GridSize itemSize)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Width = itemSize.Width;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Height = itemSize.Height;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRotated = itemSize.IsRotated;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002FE74 File Offset: 0x0002E074
		public GridSize(int width, int height, bool isRotated = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Width = width;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Height = height;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRotated = isRotated;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		public GridSize(int size, bool isRotated = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Width = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Height = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRotated = isRotated;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002FECC File Offset: 0x0002E0CC
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Effective Width: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.EffectiveWidth);
			defaultInterpolatedStringHandler.AppendLiteral(",  Effective Height: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.EffectiveHeight);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002FF1C File Offset: 0x0002E11C
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Width);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Height);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.IsRotated);
					result = stream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002FFA4 File Offset: 0x0002E1A4
		public static GridSize Deserialize(BinaryReader reader)
		{
			return new GridSize(reader.ReadInt32(), reader.ReadInt32(), reader.ReadBoolean());
		}
	}
}
