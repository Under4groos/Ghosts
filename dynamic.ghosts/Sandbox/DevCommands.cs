using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.Internal.Globals;

namespace Sandbox
{
	// Token: 0x0200016D RID: 365
	public static class DevCommands
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x00042590 File Offset: 0x00040790
		[ConCmd.AdminAttribute(null)]
		public static void setpos(float posX, float posY, float posZ, float pitch = 0f, float yaw = 0f, float roll = 0f)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("setpos", new object[]
				{
					posX,
					posY,
					posZ,
					pitch,
					yaw,
					roll
				});
				return;
			}
			if (ConsoleSystem.Caller == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("setpos: This command can only be ran by a player");
				return;
			}
			Entity ent = ConsoleSystem.Caller.Pawn;
			if (ent == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("setpos: Player is missing a Pawn entity");
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Velocity = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Position = new Vector3(posX, posY, posZ);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.EyeRotation = Rotation.From(pitch, yaw, roll);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00042664 File Offset: 0x00040864
		[ConCmd.AdminAttribute(null)]
		public static void setang(float pitch, float yaw, float roll)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("setang", new object[]
				{
					pitch,
					yaw,
					roll
				});
				return;
			}
			if (ConsoleSystem.Caller == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("setang: This command can only be ran by a player");
				return;
			}
			if (ConsoleSystem.Caller.Pawn == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("setang: Player is missing a Pawn entity");
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ConsoleSystem.Caller.Pawn.EyeRotation = Rotation.From(pitch, yaw, roll);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00042700 File Offset: 0x00040900
		[ConCmd.ServerAttribute(null)]
		public static void getpos()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("getpos");
				return;
			}
			if (ConsoleSystem.Caller == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("getpos: This command can only be ran by a player");
				return;
			}
			Entity ent = ConsoleSystem.Caller.Pawn;
			if (ent == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("getpos: Player is missing a Pawn entity");
				return;
			}
			Angles ang = ent.EyeRotation.Angles();
			Vector3 pos = ent.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("setpos {0:F2} {1:F2} {2:F2} {3:F2} {4:F2} {5:F2}", new object[]
			{
				pos.x,
				pos.y,
				pos.z,
				ang.pitch,
				ang.yaw,
				ang.roll
			}));
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x000427EA File Offset: 0x000409EA
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x000427F1 File Offset: 0x000409F1
		[ConVar.ClientAttribute(null)]
		[DefaultValue(0)]
		public static int cl_showpos { get; set; }

		// Token: 0x060010B0 RID: 4272 RVA: 0x000427FC File Offset: 0x000409FC
		[Event.FrameAttribute]
		private static void DrawPos()
		{
			if (DevCommands.cl_showpos == 0)
			{
				return;
			}
			if (!Local.Pawn.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
			defaultInterpolatedStringHandler.AppendLiteral("pos: ");
			defaultInterpolatedStringHandler.AppendFormatted<Vector3>(Local.Pawn.Position);
			debugOverlay.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), Vector2.Zero.WithX(2f), 6, Color.White, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay2 = GlobalGameNamespace.DebugOverlay;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
			defaultInterpolatedStringHandler.AppendLiteral("ang: ");
			defaultInterpolatedStringHandler.AppendFormatted<Angles>(Local.Pawn.Rotation.Angles());
			debugOverlay2.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), Vector2.Zero.WithX(2f), 7, Color.White, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay3 = GlobalGameNamespace.DebugOverlay;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
			defaultInterpolatedStringHandler.AppendLiteral("vel: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(Local.Pawn.Velocity.Length, "0.##");
			debugOverlay3.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), Vector2.Zero.WithX(2f), 8, Color.White, 0f);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00042938 File Offset: 0x00040B38
		[ConCmd.AdminAttribute(null)]
		private static void ent_fire(string targetName, string input, string value = null)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ent_fire", new object[]
				{
					targetName,
					input,
					value
				});
				return;
			}
			Entity ply = ConsoleSystem.Caller.Pawn;
			List<Entity> targets = new List<Entity>();
			if (targetName == "!picker")
			{
				Vector3 eyePosition = ply.EyePosition;
				Vector3 vector = ply.EyePosition + ply.EyeRotation.Forward * 2500f;
				Trace trace = Trace.Ray(eyePosition, vector).WithAnyTags(new string[]
				{
					"solid",
					"debris"
				}).UseHitboxes(true);
				bool flag = true;
				TraceResult tr = trace.Ignore(ply, flag).Run();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targets.Add(tr.Entity);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targets = EntityTarget.Default(targetName).GetTargets(null).ToList<Entity>();
			}
			if (!targets.Any<Entity>())
			{
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("No target entity '{0}' found!", new object[]
				{
					targetName
				}));
			}
			using (List<Entity>.Enumerator enumerator = targets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.FireInput(input, ply, value))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("Failed to fire input {0}", new object[]
						{
							input
						}));
					}
				}
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00042AC0 File Offset: 0x00040CC0
		[ConCmd.AdminAttribute(null)]
		private static void ent_remove(string targetName = "")
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ent_remove", new object[]
				{
					targetName
				});
				return;
			}
			Entity ply = ConsoleSystem.Caller.Pawn;
			List<Entity> targets = new List<Entity>();
			if (targetName == "")
			{
				Vector3 eyePosition = ply.EyePosition;
				Vector3 vector = ply.EyePosition + ply.EyeRotation.Forward * 2500f;
				Trace trace = Trace.Ray(eyePosition, vector).WithAnyTags(new string[]
				{
					"solid",
					"debris"
				}).UseHitboxes(true);
				bool flag = true;
				TraceResult tr = trace.Ignore(ply, flag).Run();
				if (tr.Entity != null)
				{
					targets.Add(tr.Entity);
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targets.Add(EntityTarget.Default(targetName).GetTargets(null).First<Entity>());
			}
			if (!targets.Any<Entity>())
			{
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("No target entity '{0}' found!", new object[]
				{
					targetName
				}));
			}
			foreach (Entity target in targets)
			{
				if (!target.IsWorld && !(target is Client))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("Removing {0}...", new object[]
					{
						target.ToString()
					}));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					target.Delete();
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00042C64 File Offset: 0x00040E64
		[ConCmd.AdminAttribute(null)]
		private static void ent_remove_all(string targetName)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ent_remove_all", new object[]
				{
					targetName
				});
				return;
			}
			List<Entity> targets = EntityTarget.Default(targetName).GetTargets(null).ToList<Entity>();
			if (!targets.Any<Entity>())
			{
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("No target entity '{0}' found!", new object[]
				{
					targetName
				}));
			}
			foreach (Entity target in targets)
			{
				if (!target.IsWorld && !(target is Player))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("Removing {0}...", new object[]
					{
						target.ToString()
					}));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					target.Delete();
				}
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00042D48 File Offset: 0x00040F48
		private static void ToggleDebugFlag(string targetName = "", EntityDebugFlags flag = EntityDebugFlags.Text)
		{
			Entity ply = ConsoleSystem.Caller.Pawn;
			List<Entity> targets = new List<Entity>();
			if (targetName == "")
			{
				Vector3 eyePosition = ply.EyePosition;
				Vector3 vector = ply.EyePosition + ply.EyeRotation.Forward * 2500f;
				Trace trace = Trace.Ray(eyePosition, vector).WithAnyTags(new string[]
				{
					"solid",
					"debris"
				}).UseHitboxes(true);
				bool flag2 = true;
				TraceResult tr = trace.Ignore(ply, flag2).Run();
				if (tr.Entity != null && !(tr.Entity is WorldEntity))
				{
					targets.Add(tr.Entity);
				}
				if (!targets.Any<Entity>())
				{
					IEnumerable<Entity> ents = Entity.FindInSphere(tr.HitPosition, 64f);
					if (ents.Any<Entity>())
					{
						targets.Add(ents.First<Entity>());
					}
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targets.AddRange(EntityTarget.Default(targetName).GetTargets(null));
			}
			foreach (Entity target in targets)
			{
				if (!target.IsWorld)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					target.DebugFlags ^= flag;
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00042EB8 File Offset: 0x000410B8
		[ConCmd.AdminAttribute(null)]
		private static void ent_text(string targetName = "")
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ent_text", new object[]
				{
					targetName
				});
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCommands.ToggleDebugFlag(targetName, EntityDebugFlags.Text);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00042EE3 File Offset: 0x000410E3
		[ConCmd.AdminAttribute(null)]
		private static void ent_bbox(string targetName = "")
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ent_bbox", new object[]
				{
					targetName
				});
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCommands.ToggleDebugFlag(targetName, EntityDebugFlags.OVERLAY_BBOX_BIT);
		}
	}
}
