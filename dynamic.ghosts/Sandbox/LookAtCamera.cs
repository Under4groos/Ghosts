using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000143 RID: 323
	public class LookAtCamera : CameraMode
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0003B9B2 File Offset: 0x00039BB2
		[Net]
		[Description("Origin of the camera")]
		public unsafe Vector3 Origin
		{
			get
			{
				return *this._repback__Origin.GetValue();
			}
			set
			{
				this._repback__Origin.SetValue(value);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003B9C1 File Offset: 0x00039BC1
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		[Net]
		[Description("Entity to look at")]
		public unsafe Entity TargetEntity
		{
			get
			{
				return *this._repback__TargetEntity.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<Entity>> repback__TargetEntity = this._repback__TargetEntity;
				EntityHandle<Entity> entityHandle = value;
				repback__TargetEntity.SetValue(entityHandle);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0003B9F9 File Offset: 0x00039BF9
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0003BA0B File Offset: 0x00039C0B
		[Net]
		[Description("Offset from the entity to look at")]
		public unsafe Vector3 TargetOffset
		{
			get
			{
				return *this._repback__TargetOffset.GetValue();
			}
			set
			{
				this._repback__TargetOffset.SetValue(value);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003BA1A File Offset: 0x00039C1A
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x0003BA28 File Offset: 0x00039C28
		[Net]
		[DefaultValue(50)]
		[Description("Min fov when target is max distance away from origin")]
		public unsafe float MinFov
		{
			get
			{
				return *this._repback__MinFov.GetValue();
			}
			set
			{
				this._repback__MinFov.SetValue(value);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003BA37 File Offset: 0x00039C37
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x0003BA45 File Offset: 0x00039C45
		[Net]
		[DefaultValue(100)]
		[Description("Max fov when target is near the origin")]
		public unsafe float MaxFov
		{
			get
			{
				return *this._repback__MaxFov.GetValue();
			}
			set
			{
				this._repback__MaxFov.SetValue(value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003BA54 File Offset: 0x00039C54
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x0003BA62 File Offset: 0x00039C62
		[Net]
		[DefaultValue(1000)]
		[Description("How far away to reach min fov")]
		public unsafe float MinFovDistance
		{
			get
			{
				return *this._repback__MinFovDistance.GetValue();
			}
			set
			{
				this._repback__MinFovDistance.SetValue(value);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003BA71 File Offset: 0x00039C71
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0003BA7F File Offset: 0x00039C7F
		[Net]
		[DefaultValue(4f)]
		[Description("How quick to lerp to target")]
		public unsafe float LerpSpeed
		{
			get
			{
				return *this._repback__LerpSpeed.GetValue();
			}
			set
			{
				this._repback__LerpSpeed.SetValue(value);
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003BA8E File Offset: 0x00039C8E
		protected virtual Vector3 GetTargetPos()
		{
			if (!this.TargetEntity.IsValid())
			{
				return Vector3.Zero;
			}
			return this.TargetEntity.Position + this.TargetOffset;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003BAB9 File Offset: 0x00039CB9
		public override void Activated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastTargetPos = this.GetTargetPos();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003BACC File Offset: 0x00039CCC
		public override void Update()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.Origin;
			Vector3 targetPos = this.GetTargetPos();
			if (this.LerpSpeed > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetPos = this.lastTargetPos.LerpTo(targetPos, Time.Delta * this.LerpSpeed);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastTargetPos = targetPos;
			Vector3 targetDelta = targetPos - this.Origin;
			float targetDistance = targetDelta.Length;
			Vector3 targetDirection = targetDelta.Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Rotation.From(new Angles(((float)Math.Asin((double)targetDirection.z)).RadianToDegree() * -1f, ((float)Math.Atan2((double)targetDirection.y, (double)targetDirection.x)).RadianToDegree(), 0f));
			float fovDelta = (this.MinFovDistance > 0f) ? (targetDistance / this.MinFovDistance) : 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = this.MaxFov.LerpTo(this.MinFov, fovDelta, true);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003BBDC File Offset: 0x00039DDC
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<Vector3>>(ref this._repback__Origin, "Origin", false, false);
			builder.Register<VarUnmanaged<EntityHandle<Entity>>>(ref this._repback__TargetEntity, "TargetEntity", false, false);
			builder.Register<VarUnmanaged<Vector3>>(ref this._repback__TargetOffset, "TargetOffset", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MinFov, "MinFov", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MaxFov, "MaxFov", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MinFovDistance, "MinFovDistance", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__LerpSpeed, "LerpSpeed", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400049E RID: 1182
		private Vector3 lastTargetPos;

		// Token: 0x0400049F RID: 1183
		private VarUnmanaged<Vector3> _repback__Origin = new VarUnmanaged<Vector3>();

		// Token: 0x040004A0 RID: 1184
		private VarUnmanaged<EntityHandle<Entity>> _repback__TargetEntity = new VarUnmanaged<EntityHandle<Entity>>();

		// Token: 0x040004A1 RID: 1185
		private VarUnmanaged<Vector3> _repback__TargetOffset = new VarUnmanaged<Vector3>();

		// Token: 0x040004A2 RID: 1186
		private VarUnmanaged<float> _repback__MinFov = new VarUnmanaged<float>(50f);

		// Token: 0x040004A3 RID: 1187
		private VarUnmanaged<float> _repback__MaxFov = new VarUnmanaged<float>(100f);

		// Token: 0x040004A4 RID: 1188
		private VarUnmanaged<float> _repback__MinFovDistance = new VarUnmanaged<float>(1000f);

		// Token: 0x040004A5 RID: 1189
		private VarUnmanaged<float> _repback__LerpSpeed = new VarUnmanaged<float>(4f);
	}
}
