using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x02000049 RID: 73
	public abstract class StalkerResource : GameResource
	{
		// Token: 0x06000307 RID: 775 RVA: 0x00012004 File Offset: 0x00010204
		protected override void PostLoad()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(base.ResourceName);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerResource.assetCache[base.ResourceName] = this;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00012034 File Offset: 0x00010234
		public static T Get<T>(string name) where T : StalkerResource
		{
			if (!StalkerResource.assetCache.ContainsKey(name))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Error(FormattableStringFactory.Create("Asset Cache has no value for key {0}", new object[]
				{
					name
				}));
				return default(T);
			}
			return StalkerResource.assetCache[name] as T;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00012090 File Offset: 0x00010290
		public static List<T> GetAllResourcesOfType<T>() where T : StalkerResource
		{
			List<T> list = new List<T>();
			foreach (StalkerResource stalkerResource in StalkerResource.assetCache.Values)
			{
				T tValue = stalkerResource as T;
				if (tValue != null)
				{
					list.Add(tValue);
				}
			}
			return list;
		}

		// Token: 0x040000F0 RID: 240
		private static readonly Dictionary<string, StalkerResource> assetCache = new Dictionary<string, StalkerResource>();
	}
}
