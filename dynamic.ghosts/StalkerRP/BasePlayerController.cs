using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200002B RID: 43
	public abstract class BasePlayerController : PawnController
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000A00D File Offset: 0x0000820D
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00009FE5 File Offset: 0x000081E5
		[ConVar.ReplicatedAttribute("debug_playercontroller")]
		public static bool Debug
		{
			get
			{
				return BasePlayerController._repback__Debug;
			}
			set
			{
				if (BasePlayerController._repback__Debug == value)
				{
					return;
				}
				BasePlayerController._repback__Debug = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("debug_playercontroller", value);
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A014 File Offset: 0x00008214
		[Description("Traces the bbox and returns the trace result. LiftFeet will move the start position up by this amount, while keeping the top of the bbox at the same  position. This is good when tracing down because you won't be tracing through the ceiling above.")]
		public virtual TraceResult TraceBBox(Vector3 start, Vector3 end, Vector3 mins, Vector3 maxs, float liftFeet = 0f)
		{
			if (liftFeet > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				start += Vector3.Up * liftFeet;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				maxs = maxs.WithZ(maxs.z - liftFeet);
			}
			Vector3 vector = start + this.TraceOffset;
			Vector3 vector2 = end + this.TraceOffset;
			Trace trace = Trace.Ray(vector, vector2).Size(mins, maxs).WithAnyTags(new string[]
			{
				"solid",
				"playerclip",
				"passbullets",
				"player"
			});
			Entity pawn = base.Pawn;
			bool flag = true;
			TraceResult tr = trace.Ignore(pawn, flag).Run();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.EndPosition -= this.TraceOffset;
			return tr;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A0FB File Offset: 0x000082FB
		[Description("This calls TraceBBox with the right sized bbox. You should derive this in your controller if you  want to use the built in functions")]
		public virtual TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0f)
		{
			return this.TraceBBox(start, end, Vector3.One * -1f, Vector3.One, liftFeet);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A11A File Offset: 0x0000831A
		[Description("This is temporary, get the hull size for the player's collision")]
		public virtual BBox GetHull()
		{
			return new BBox(-10f, 10f);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A135 File Offset: 0x00008335
		public override void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameSimulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
		}

		// Token: 0x04000080 RID: 128
		public Vector3 TraceOffset;

		// Token: 0x04000081 RID: 129
		public static bool _repback__Debug;
	}
}
