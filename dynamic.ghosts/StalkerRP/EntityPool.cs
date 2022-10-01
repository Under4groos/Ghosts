using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000046 RID: 70
	public class EntityPool<T> where T : Entity, new()
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00011D3E File Offset: 0x0000FF3E
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00011D46 File Offset: 0x0000FF46
		[DefaultValue(200)]
		public virtual int MaxActiveEntities { get; set; } = 200;

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00011D4F File Offset: 0x0000FF4F
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00011D57 File Offset: 0x0000FF57
		[DefaultValue(true)]
		public virtual bool CreateEntitiesOnCacheEmpty { get; set; } = true;

		// Token: 0x060002FB RID: 763 RVA: 0x00011D60 File Offset: 0x0000FF60
		public T Request()
		{
			if (this.ActivePool.Count >= this.MaxActiveEntities)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Retire(this.ActivePool.First<T>());
			}
			if (this.CachePool.Count > 0)
			{
				T ent = this.CachePool.First<T>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CachePool.Remove(ent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActivePool.Add(ent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnRequested(ent);
				return ent;
			}
			if (this.CreateEntitiesOnCacheEmpty)
			{
				T ent2 = Activator.CreateInstance<T>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActivePool.Add(ent2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnRequested(ent2);
				return ent2;
			}
			return default(T);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011E1A File Offset: 0x0001001A
		protected virtual void OnRetired(T ent)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011E1C File Offset: 0x0001001C
		protected virtual void OnRequested(T ent)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011E1E File Offset: 0x0001001E
		public bool Retire(T ent)
		{
			if (this.ActivePool.Remove(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CachePool.Add(ent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnRetired(ent);
				return true;
			}
			return false;
		}

		// Token: 0x040000EC RID: 236
		protected readonly HashSet<T> ActivePool = new HashSet<T>();

		// Token: 0x040000ED RID: 237
		protected readonly List<T> CachePool = new List<T>();
	}
}
