using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000011 RID: 17
	public class EntityTrigger<T> : ModelEntity, ITrigger where T : Entity
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00007E76 File Offset: 0x00006076
		public bool IsTriggerBeingTouch
		{
			get
			{
				return this.TriggeringEntities.Count > 0;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007E86 File Offset: 0x00006086
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetTriggerSize(16f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007EC4 File Offset: 0x000060C4
		[Description("Set the trigger radius. Default is 16.")]
		public virtual void SetTriggerSize(float radius)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromSphere(PhysicsMotionType.Keyframed, this.Position, radius);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.AutoSleep = false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007EEB File Offset: 0x000060EB
		public virtual bool IsTriggering(T entity)
		{
			return this.TriggeringEntities.Contains(entity);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007EF9 File Offset: 0x000060F9
		protected virtual bool CanTouchTrigger(T entity)
		{
			return true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007EFC File Offset: 0x000060FC
		protected virtual void OnTriggerTouched(T other)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007EFE File Offset: 0x000060FE
		protected virtual void OnTriggerLeft(T other)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007F00 File Offset: 0x00006100
		public override void StartTouch(Entity other)
		{
			T ent = other as T;
			if (ent == null)
			{
				return;
			}
			if (this.CanTouchTrigger(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TriggeringEntities.Add(ent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnTriggerTouched(ent);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007F4C File Offset: 0x0000614C
		public override void EndTouch(Entity other)
		{
			T ent = other as T;
			if (ent == null)
			{
				return;
			}
			if (this.TriggeringEntities.Remove(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnTriggerLeft(ent);
			}
		}

		// Token: 0x04000034 RID: 52
		public readonly HashSet<T> TriggeringEntities = new HashSet<T>();
	}
}
