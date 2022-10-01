using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000189 RID: 393
	[Library]
	public abstract class BasePlayerController : PawnController
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00049ED1 File Offset: 0x000480D1
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00049EA9 File Offset: 0x000480A9
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

		// Token: 0x0600130B RID: 4875 RVA: 0x00049ED8 File Offset: 0x000480D8
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

		// Token: 0x0600130C RID: 4876 RVA: 0x00049FBF File Offset: 0x000481BF
		[Description("This calls TraceBBox with the right sized bbox. You should derive this in your controller if you  want to use the built in functions")]
		public virtual TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0f)
		{
			return this.TraceBBox(start, end, Vector3.One * -1f, Vector3.One, liftFeet);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00049FDE File Offset: 0x000481DE
		[Description("This is temporary, get the hull size for the player's collision")]
		public virtual BBox GetHull()
		{
			return new BBox(-10f, 10f);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00049FF9 File Offset: 0x000481F9
		public override void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameSimulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
		}

		// Token: 0x04000617 RID: 1559
		public Vector3 TraceOffset;

		// Token: 0x04000618 RID: 1560
		public static bool _repback__Debug;
	}
}
