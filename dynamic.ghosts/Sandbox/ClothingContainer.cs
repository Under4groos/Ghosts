using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000166 RID: 358
	[Description("Holds a collection of clothing items. Won't let you add items that aren't compatible.")]
	public class ClothingContainer
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x00040E78 File Offset: 0x0003F078
		[Description("Add a clothing item if we don't already contain it, else remove it")]
		public void Toggle(Clothing clothing)
		{
			if (this.Has(clothing))
			{
				this.Remove(clothing);
				return;
			}
			this.Add(clothing);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00040E94 File Offset: 0x0003F094
		[Description("Add clothing item")]
		private void Add(Clothing clothing)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Clothing.RemoveAll((Clothing x) => !x.CanBeWornWith(clothing));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Clothing.Add(clothing);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00040EE4 File Offset: 0x0003F0E4
		[Description("Load the clothing from this client's data. This is a different entry point than just calling Deserialize directly because if we have inventory based skins at some point, we can validate ownership here")]
		public void LoadFromClient(Client cl)
		{
			string data = cl.GetClientData("avatar", null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Deserialize(data);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00040F0A File Offset: 0x0003F10A
		[Description("Remove clothing item")]
		private void Remove(Clothing clothing)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Clothing.Remove(clothing);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00040F1E File Offset: 0x0003F11E
		[Description("Returns true if we have this clothing item")]
		public bool Has(Clothing clothing)
		{
			return this.Clothing.Contains(clothing);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00040F2C File Offset: 0x0003F12C
		[Description("Return a list of bodygroups and what their value should be")]
		[return: TupleElementNames(new string[]
		{
			"name",
			"value"
		})]
		public IEnumerable<ValueTuple<string, int>> GetBodyGroups()
		{
			Clothing.BodyGroups mask = (from x in this.Clothing
			select x.HideBody).DefaultIfEmpty<Clothing.BodyGroups>().Aggregate((Clothing.BodyGroups a, Clothing.BodyGroups b) => a | b);
			yield return new ValueTuple<string, int>("head", ((mask & Sandbox.Clothing.BodyGroups.Head) != (Clothing.BodyGroups)0) ? 1 : 0);
			yield return new ValueTuple<string, int>("Chest", ((mask & Sandbox.Clothing.BodyGroups.Chest) != (Clothing.BodyGroups)0) ? 1 : 0);
			yield return new ValueTuple<string, int>("Legs", ((mask & Sandbox.Clothing.BodyGroups.Legs) != (Clothing.BodyGroups)0) ? 1 : 0);
			yield return new ValueTuple<string, int>("Hands", ((mask & Sandbox.Clothing.BodyGroups.Hands) != (Clothing.BodyGroups)0) ? 1 : 0);
			yield return new ValueTuple<string, int>("Feet", ((mask & Sandbox.Clothing.BodyGroups.Feet) != (Clothing.BodyGroups)0) ? 1 : 0);
			yield break;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00040F3C File Offset: 0x0003F13C
		[Description("Serialize to Json")]
		public string Serialize()
		{
			return JsonSerializer.Serialize<IEnumerable<ClothingContainer.Entry>>(from x in this.Clothing
			select new ClothingContainer.Entry
			{
				Id = x.ResourceId
			}, null);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00040F70 File Offset: 0x0003F170
		[Description("Deserialize from Json")]
		public void Deserialize(string json)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Clothing.Clear();
			if (string.IsNullOrWhiteSpace(json))
			{
				return;
			}
			try
			{
				foreach (ClothingContainer.Entry entry in JsonSerializer.Deserialize<ClothingContainer.Entry[]>(json, null))
				{
					Clothing item = GlobalGameNamespace.ResourceLibrary.Get<Clothing>(entry.Id);
					if (item != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Add(item);
					}
				}
			}
			catch (Exception e)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(e, "Error deserailizing clothing");
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00041004 File Offset: 0x0003F204
		public void ClearEntities()
		{
			foreach (Entity entity in this.ClothingModels)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ClothingModels.Clear();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004106C File Offset: 0x0003F26C
		[Description("Dress this citizen with clothes defined inside this class. We'll save the created entities in ClothingModels. All clothing entities are tagged with \"clothes\".")]
		public void DressEntity(AnimatedEntity citizen, bool hideInFirstPerson = true, bool castShadowsInFirstPerson = true)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			citizen.SetMaterialGroup("default");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ClearEntities();
			Material SkinMaterial = (from x in this.Clothing
			select x.SkinMaterial into x
			where !string.IsNullOrWhiteSpace(x)
			select Material.Load(x)).FirstOrDefault<Material>();
			Material EyesMaterial = (from x in this.Clothing
			select x.EyesMaterial into x
			where !string.IsNullOrWhiteSpace(x)
			select Material.Load(x)).FirstOrDefault<Material>();
			if (SkinMaterial != null)
			{
				citizen.SetMaterialOverride(SkinMaterial, "skin");
			}
			if (EyesMaterial != null)
			{
				citizen.SetMaterialOverride(EyesMaterial, "eyes");
			}
			foreach (Clothing c in this.Clothing)
			{
				if (c.Model == "models/citizen/citizen.vmdl")
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					citizen.SetMaterialGroup(c.MaterialGroup);
				}
				else
				{
					AnimatedEntity anim = new AnimatedEntity(c.Model, citizen);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					anim.Tags.Add("clothes");
					if (SkinMaterial != null)
					{
						anim.SetMaterialOverride(SkinMaterial, "skin");
					}
					if (EyesMaterial != null)
					{
						anim.SetMaterialOverride(EyesMaterial, "eyes");
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					anim.EnableHideInFirstPerson = hideInFirstPerson;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					anim.EnableShadowInFirstPerson = castShadowsInFirstPerson;
					if (!string.IsNullOrEmpty(c.MaterialGroup))
					{
						anim.SetMaterialGroup(c.MaterialGroup);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ClothingModels.Add(anim);
				}
			}
			foreach (ValueTuple<string, int> group in this.GetBodyGroups())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				citizen.SetBodyGroup(group.Item1, group.Item2);
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000412F0 File Offset: 0x0003F4F0
		[Description("Dress this citizen with clothes defined inside this class. We'll save the created entities in ClothingModels. All clothing entities are tagged with \"clothes\".")]
		public List<SceneModel> DressSceneObject(SceneModel citizen)
		{
			List<SceneModel> created = new List<SceneModel>();
			SceneWorld world = citizen.World;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			citizen.SetMaterialGroup("default");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			citizen.SetMaterialOverride(null);
			Material SkinMaterial = (from x in this.Clothing
			select x.SkinMaterial into x
			where !string.IsNullOrWhiteSpace(x)
			select Material.Load(x)).FirstOrDefault<Material>();
			Material EyesMaterial = (from x in this.Clothing
			select x.EyesMaterial into x
			where !string.IsNullOrWhiteSpace(x)
			select Material.Load(x)).FirstOrDefault<Material>();
			if (SkinMaterial != null)
			{
				citizen.SetMaterialOverride(SkinMaterial, "skin", 1);
			}
			if (EyesMaterial != null)
			{
				citizen.SetMaterialOverride(EyesMaterial, "eyes", 1);
			}
			foreach (Clothing c in this.Clothing)
			{
				if (!string.IsNullOrEmpty(c.Model) && string.IsNullOrEmpty(c.SkinMaterial))
				{
					Model model = Model.Load(c.Model);
					SceneModel anim = new SceneModel(world, model, citizen.Transform);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					created.Add(anim);
					if (!string.IsNullOrEmpty(c.MaterialGroup))
					{
						anim.SetMaterialGroup(c.MaterialGroup);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					citizen.AddChild("clothing", anim);
					if (SkinMaterial != null)
					{
						anim.SetMaterialOverride(SkinMaterial, "skin", 1);
					}
					if (EyesMaterial != null)
					{
						anim.SetMaterialOverride(EyesMaterial, "eyes", 1);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					anim.Update(0.1f);
				}
			}
			foreach (ValueTuple<string, int> group in this.GetBodyGroups())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				citizen.SetBodyGroup(group.Item1, group.Item2);
			}
			return created;
		}

		// Token: 0x0400052E RID: 1326
		public List<Clothing> Clothing = new List<Clothing>();

		// Token: 0x0400052F RID: 1327
		private List<AnimatedEntity> ClothingModels = new List<AnimatedEntity>();

		// Token: 0x0200022A RID: 554
		public struct Entry
		{
			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06001937 RID: 6455 RVA: 0x00067744 File Offset: 0x00065944
			// (set) Token: 0x06001938 RID: 6456 RVA: 0x0006774C File Offset: 0x0006594C
			[JsonPropertyName("id")]
			public int Id { readonly get; set; }
		}
	}
}
