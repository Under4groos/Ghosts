using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200016B RID: 363
	[Description("Handle breaking a prop into bits")]
	public static class Breakables
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x000418B4 File Offset: 0x0003FAB4
		public static void ApplyBreakCommands(Breakables.Result result)
		{
			ModelEntity ent = result.Source as ModelEntity;
			if (ent == null || ent.Model == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string[]> kv in ent.Model.GetBreakCommands())
			{
				TypeDescription description = GlobalGameNamespace.TypeLibrary.GetDescription(kv.Key);
				Type type = (description != null) ? description.TargetType : null;
				if (!(type == null))
				{
					foreach (string cmdData in kv.Value)
					{
						try
						{
							IModelBreakCommand bcInst = JsonSerializer.Deserialize(cmdData, type, Breakables.jsonOptions) as IModelBreakCommand;
							if (bcInst != null)
							{
								RuntimeHelpers.EnsureSufficientExecutionStack();
								bcInst.OnBreak(result);
							}
						}
						catch (Exception e)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							GlobalGameNamespace.Log.Error(e);
						}
					}
				}
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000419B8 File Offset: 0x0003FBB8
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x000419BF File Offset: 0x0003FBBF
		[ConVar.ServerAttribute("gibs_max")]
		[DefaultValue(256)]
		public static int MaxGibs { get; set; } = 256;

		// Token: 0x06001084 RID: 4228 RVA: 0x000419C8 File Offset: 0x0003FBC8
		public static void Break(Entity ent, Breakables.Result result = null)
		{
			ModelEntity modelEnt = ent as ModelEntity;
			if (modelEnt != null && modelEnt.IsValid)
			{
				if (result != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					result.Source = ent;
					if (result.Params.DamagePositon.IsNearlyZero(1E-45f))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						result.Params.DamagePositon = modelEnt.CollisionWorldSpaceCenter;
					}
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Breakables.Break(modelEnt.Model, modelEnt.Position, modelEnt.Rotation, modelEnt.Scale, modelEnt.RenderColor, result, modelEnt.PhysicsBody);
				if (result != null)
				{
					Breakables.ApplyBreakCommands(result);
				}
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00041A5C File Offset: 0x0003FC5C
		public static void Break(Model model, Vector3 pos, Rotation rot, float scale, Color color, Breakables.Result result = null, PhysicsBody sourcePhysics = null)
		{
			if (model == null || model.IsError)
			{
				return;
			}
			bool genericGibsSpawned = false;
			ModelBreakPiece[] breakList = model.GetData<ModelBreakPiece[]>();
			bool hasAnyBreakParticles = model.GetBreakCommands().ContainsKey("break_create_particle");
			if ((breakList == null || breakList.Length == 0) && !hasAnyBreakParticles)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				genericGibsSpawned = true;
				List<ModelBreakPiece> pieces = new List<ModelBreakPiece>();
				Surface surface = Surface.FindByName((sourcePhysics != null) ? sourcePhysics.GetDominantSurface() : null);
				if (surface == null)
				{
					surface = Surface.FindByName("default");
				}
				if (surface != null)
				{
					string breakSnd = surface.Breakables.BreakSound;
					Surface surf = surface.GetBaseSurface();
					while (string.IsNullOrWhiteSpace(breakSnd) && surf != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						breakSnd = surf.Breakables.BreakSound;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						surf = surf.GetBaseSurface();
					}
					if (!string.IsNullOrEmpty(breakSnd))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sound.FromWorld(breakSnd, pos);
					}
				}
				int num = (model.Bounds.Size.x * model.Bounds.Size.y + model.Bounds.Size.y * model.Bounds.Size.z + model.Bounds.Size.z * model.Bounds.Size.x).CeilToInt() / 864;
				for (int i = 0; i < num; i++)
				{
					ModelBreakPiece piece = default(ModelBreakPiece);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					piece.Model = surface.GetRandomGib();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					piece.Offset = model.Bounds.RandomPointInside;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					piece.CollisionTags = "debris";
					RuntimeHelpers.EnsureSufficientExecutionStack();
					piece.FadeTime = 3f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					pieces.Add(piece);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				breakList = pieces.ToArray();
			}
			if (breakList == null || breakList.Length == 0)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Breakables.CurrentGibs.RemoveAll((Entity x) => !x.IsValid());
			if (Breakables.MaxGibs > 0 && Breakables.CurrentGibs.Count + breakList.Length >= Breakables.MaxGibs)
			{
				int toRemove = Breakables.CurrentGibs.Count + breakList.Length - Breakables.MaxGibs;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				toRemove = Math.Min(toRemove, Breakables.CurrentGibs.Count);
				for (int j = 0; j < toRemove; j++)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Breakables.CurrentGibs[j].Delete();
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Breakables.CurrentGibs.RemoveRange(0, toRemove);
			}
			foreach (ModelBreakPiece piece2 in breakList)
			{
				if (Breakables.MaxGibs >= 0 && Breakables.CurrentGibs.Count >= Breakables.MaxGibs)
				{
					return;
				}
				Model mdl = Model.Load(piece2.Model);
				Transform offset = mdl.GetAttachment("placementOrigin") ?? Transform.Zero;
				PropGib gib = new PropGib
				{
					Position = pos + rot * ((piece2.Offset - offset.Position) * scale),
					Rotation = rot,
					Scale = scale,
					RenderColor = color,
					Invulnerable = 0.2f,
					BreakpieceName = piece2.PieceName
				};
				RuntimeHelpers.EnsureSufficientExecutionStack();
				gib.Tags.Add("gib");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				gib.Model = mdl;
				if (result != null && result.Source != null)
				{
					ModelEntity mdlEnt = result.Source as ModelEntity;
					if (mdlEnt != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						gib.CopyMaterialGroup(mdlEnt);
					}
				}
				if (!string.IsNullOrWhiteSpace(piece2.CollisionTags))
				{
					foreach (string p in piece2.CollisionTags.Split(' ', StringSplitOptions.None))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						gib.Tags.Add(p);
					}
				}
				PhysicsBody phys = gib.PhysicsBody;
				if (phys != null && sourcePhysics != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					phys.Velocity = sourcePhysics.GetVelocityAtPoint(phys.Position);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					phys.AngularVelocity = sourcePhysics.AngularVelocity;
				}
				if (piece2.FadeTime > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Breakables.FadeAsync(gib, piece2.FadeTime);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (result != null)
				{
					result.AddProp(gib);
				}
				if (Breakables.MaxGibs > 0)
				{
					Breakables.CurrentGibs.Add(gib);
				}
			}
			if (genericGibsSpawned && result != null)
			{
				foreach (ModelEntity modelEntity in result.Props)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					modelEntity.AngularVelocity = Angles.Random * 256f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					modelEntity.Velocity = Vector3.Random * 100f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					modelEntity.Rotation = Rotation.Random;
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00041FA4 File Offset: 0x000401A4
		private static Task FadeAsync(Prop gib, float fadeTime)
		{
			Breakables.<FadeAsync>d__9 <FadeAsync>d__;
			<FadeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FadeAsync>d__.gib = gib;
			<FadeAsync>d__.fadeTime = fadeTime;
			<FadeAsync>d__.<>1__state = -1;
			<FadeAsync>d__.<>t__builder.Start<Breakables.<FadeAsync>d__9>(ref <FadeAsync>d__);
			return <FadeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000532 RID: 1330
		private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			ReadCommentHandling = JsonCommentHandling.Skip,
			PropertyNameCaseInsensitive = true,
			IncludeFields = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x04000533 RID: 1331
		internal static List<Entity> CurrentGibs = new List<Entity>();

		// Token: 0x02000230 RID: 560
		public class Result
		{
			// Token: 0x0600195B RID: 6491 RVA: 0x00067A7D File Offset: 0x00065C7D
			public void AddProp(ModelEntity prop)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Props.Add(prop);
			}

			// Token: 0x0600195C RID: 6492 RVA: 0x00067A90 File Offset: 0x00065C90
			public void CopyParamsFrom(DamageInfo dmgInfo)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Params.DamagePositon = dmgInfo.Position;
			}

			// Token: 0x0400091F RID: 2335
			public Entity Source;

			// Token: 0x04000920 RID: 2336
			public Breakables.Result.BreakableParams Params;

			// Token: 0x04000921 RID: 2337
			public List<ModelEntity> Props = new List<ModelEntity>();

			// Token: 0x02000275 RID: 629
			public struct BreakableParams
			{
				// Token: 0x04000A37 RID: 2615
				public Vector3 DamagePositon;
			}
		}
	}
}
