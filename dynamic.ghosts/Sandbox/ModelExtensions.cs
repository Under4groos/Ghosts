using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ModelDoc;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200014E RID: 334
	[Description("Extensions for Model")]
	public static class ModelExtensions
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		[Description("Returns all game data nodes that derive from given class/interface, and are present on the model. Does NOT support data nodes that allow multiple entries.")]
		public static List<T> GetAllData<T>(this Model self)
		{
			List<T> list = new List<T>();
			foreach (TypeDescription description in GlobalGameNamespace.TypeLibrary.GetDescriptions<T>())
			{
				GameDataAttribute att = description.GetAttribute<GameDataAttribute>();
				object data;
				if (att != null && att.AllowMultiple && self.TryGetData(description.TargetType, out data))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					list.Add((T)((object)data));
				}
			}
			return list;
		}
	}
}
