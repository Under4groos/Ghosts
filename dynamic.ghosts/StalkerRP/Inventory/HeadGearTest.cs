using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory
{
	// Token: 0x02000101 RID: 257
	[Title("Head Lamp")]
	[Spawnable]
	public class HeadGearTest : ItemEntity
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x0002F974 File Offset: 0x0002DB74
		public override void Spawn()
		{
			Item item = new Item("item_headgear_head_lamp");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.InitializeFromAsset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetItem(item);
		}
	}
}
