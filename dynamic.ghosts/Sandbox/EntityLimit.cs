using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000160 RID: 352
	[Description("A class that limits the amount of entities.")]
	public class EntityLimit
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003F78B File Offset: 0x0003D98B
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003F793 File Offset: 0x0003D993
		[DefaultValue(2)]
		[Description("Maximum entities in this list before we start deleting")]
		public virtual int MaxTotal { get; set; } = 2;

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0003F79C File Offset: 0x0003D99C
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
		[Description("List of entities currently in this shit")]
		public List<Entity> List { get; protected set; } = new List<Entity>();

		// Token: 0x06001002 RID: 4098 RVA: 0x0003F7AD File Offset: 0x0003D9AD
		[Description("Watch an entity, contribute to the count and delete when its their turn")]
		public void Watch(ModelEntity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.List.Add(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Maintain();
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0003F7CC File Offset: 0x0003D9CC
		[Description("Maintain the list, delete entities if they need deleting")]
		private void Maintain()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.List.RemoveAll((Entity x) => !x.IsValid());
			while (this.List.Count > this.MaxTotal)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Retire(this.List.First<Entity>());
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003F834 File Offset: 0x0003DA34
		[Description("Delete this entity and remove it from the list")]
		public void Retire(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.List.RemoveAll((Entity x) => x == ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Delete();
		}
	}
}
