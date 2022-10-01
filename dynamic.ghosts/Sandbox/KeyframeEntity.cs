using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Sandbox
{
	// Token: 0x02000177 RID: 375
	[Description("An entity that is moved programmatically. Like an elevator or a linear smashing star wars garbage compactor")]
	public class KeyframeEntity : AnimatedEntity
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x00044304 File Offset: 0x00042504
		[Description("Move to given transform in given amount of time")]
		public Task<bool> KeyframeTo(Transform target, float seconds, Easing.Function easing = null)
		{
			KeyframeEntity.<KeyframeTo>d__2 <KeyframeTo>d__;
			<KeyframeTo>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<KeyframeTo>d__.<>4__this = this;
			<KeyframeTo>d__.target = target;
			<KeyframeTo>d__.seconds = seconds;
			<KeyframeTo>d__.easing = easing;
			<KeyframeTo>d__.<>1__state = -1;
			<KeyframeTo>d__.<>t__builder.Start<KeyframeEntity.<KeyframeTo>d__2>(ref <KeyframeTo>d__);
			return <KeyframeTo>d__.<>t__builder.Task;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0004435F File Offset: 0x0004255F
		[Description("Used by KeyframeTo methods to try to move to a given transform")]
		public virtual bool TryKeyframeTo(Transform pos)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transform = pos;
			return true;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00044370 File Offset: 0x00042570
		[Description("Move to a given local position in given amount of time")]
		public Task<bool> LocalKeyframeTo(Vector3 deltaTarget, float seconds, Easing.Function easing = null)
		{
			KeyframeEntity.<LocalKeyframeTo>d__5 <LocalKeyframeTo>d__;
			<LocalKeyframeTo>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LocalKeyframeTo>d__.<>4__this = this;
			<LocalKeyframeTo>d__.deltaTarget = deltaTarget;
			<LocalKeyframeTo>d__.seconds = seconds;
			<LocalKeyframeTo>d__.easing = easing;
			<LocalKeyframeTo>d__.<>1__state = -1;
			<LocalKeyframeTo>d__.<>t__builder.Start<KeyframeEntity.<LocalKeyframeTo>d__5>(ref <LocalKeyframeTo>d__);
			return <LocalKeyframeTo>d__.<>t__builder.Task;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000443CB File Offset: 0x000425CB
		[Description("Used by KeyframeTo methods to try to move to a given local position")]
		public virtual bool TryLocalKeyframeTo(Vector3 pos, float delta)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalVelocity = (pos - this.LocalPosition) / Math.Max(delta, 0.001f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalPosition = pos;
			return true;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00044404 File Offset: 0x00042604
		[Description("Rotate to a given local rotation in given amount of time")]
		public Task<bool> LocalRotateKeyframeTo(Rotation localTarget, float seconds, Easing.Function easing = null)
		{
			KeyframeEntity.<LocalRotateKeyframeTo>d__8 <LocalRotateKeyframeTo>d__;
			<LocalRotateKeyframeTo>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LocalRotateKeyframeTo>d__.<>4__this = this;
			<LocalRotateKeyframeTo>d__.localTarget = localTarget;
			<LocalRotateKeyframeTo>d__.seconds = seconds;
			<LocalRotateKeyframeTo>d__.easing = easing;
			<LocalRotateKeyframeTo>d__.<>1__state = -1;
			<LocalRotateKeyframeTo>d__.<>t__builder.Start<KeyframeEntity.<LocalRotateKeyframeTo>d__8>(ref <LocalRotateKeyframeTo>d__);
			return <LocalRotateKeyframeTo>d__.<>t__builder.Task;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004445F File Offset: 0x0004265F
		[Description("Used by LocalRotateKeyframeTo to try to rotate to a given rotation")]
		public virtual bool TryLocalRotateTo(Rotation pos)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalRotation = pos;
			return true;
		}

		// Token: 0x04000556 RID: 1366
		private int movement;

		// Token: 0x04000557 RID: 1367
		private int movementTick;

		// Token: 0x04000558 RID: 1368
		private int local_movement;

		// Token: 0x04000559 RID: 1369
		private int local_rot_movement;
	}
}
