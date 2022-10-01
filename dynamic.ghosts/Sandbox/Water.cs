using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000197 RID: 407
	[Library("water")]
	public class Water : ModelEntity
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00052371 File Offset: 0x00050571
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0005237E File Offset: 0x0005057E
		[Property]
		[ResourceType("vmat")]
		[Net]
		[DefaultValue("materials/shadertest/test_water.vmat")]
		[Description("Material to use for water")]
		public string WaterMaterial
		{
			get
			{
				return this._repback__WaterMaterial.GetValue();
			}
			set
			{
				this._repback__WaterMaterial.SetValue(value);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0005238C File Offset: 0x0005058C
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00052394 File Offset: 0x00050594
		[Property]
		[DefaultValue(true)]
		public bool EnableReflection { get; set; } = true;

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0005239D File Offset: 0x0005059D
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x000523A5 File Offset: 0x000505A5
		[Property]
		[DefaultValue(true)]
		public bool EnableRipples { get; set; } = true;

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000523AE File Offset: 0x000505AE
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x000523B6 File Offset: 0x000505B6
		[Property]
		[DefaultValue(true)]
		public bool EnableShadows { get; set; } = true;

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000523BF File Offset: 0x000505BF
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x000523C7 File Offset: 0x000505C7
		[Property]
		[DefaultValue(true)]
		public bool EnableFog { get; set; } = true;

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000523D0 File Offset: 0x000505D0
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x000523D8 File Offset: 0x000505D8
		[Property]
		[DefaultValue(true)]
		public bool EnableRefraction { get; set; } = true;

		// Token: 0x06001448 RID: 5192 RVA: 0x000523E4 File Offset: 0x000505E4
		public Water()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("water");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterController.WaterEntity = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouch = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouchPersists = true;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00052473 File Offset: 0x00050673
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			WaterSceneObject waterSceneObject = this.WaterSceneObject;
			if (waterSceneObject != null)
			{
				waterSceneObject.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterController.WaterEntity = null;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000524A7 File Offset: 0x000506A7
		public override void Touch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Touch(other);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterController.Touch(other);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000524C6 File Offset: 0x000506C6
		public override void EndTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EndTouch(other);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterController.EndTouch(other);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000524E5 File Offset: 0x000506E5
		public override void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StartTouch(other);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterController.StartTouch(other);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00052504 File Offset: 0x00050704
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateWaterSceneObject();
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0005251C File Offset: 0x0005071C
		private void CreateWaterSceneObject()
		{
			if (this.WaterMaterial == null || this.WaterMaterial.Length == 0)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterSceneObject = new WaterSceneObject(GlobalGameNamespace.Map.Scene, this, Material.Load(this.WaterMaterial))
			{
				Transform = base.Transform,
				Position = this.Position,
				Bounds = base.CollisionBounds + this.Position,
				RenderBounds = base.CollisionBounds + this.Position
			};
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000525AB File Offset: 0x000507AB
		[Event.FrameAttribute]
		protected virtual void Think()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSceneObject(this.WaterSceneObject);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000525BE File Offset: 0x000507BE
		[Description("Keep the scene object updated. By default this moves the transform to match this entity's transform and updates the bounds to the new position.")]
		public virtual void UpdateSceneObject(SceneObject obj)
		{
			if (this.WaterSceneObject == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterSceneObject.Update();
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000525DC File Offset: 0x000507DC
		public override void TakeDamage(DamageInfo info)
		{
			if (Host.IsClient)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			if (info.Flags.HasFlag(DamageFlags.Bullet))
			{
				this.AddRipple(info.Position, Rand.Float(info.Damage, info.Damage * 4f));
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0005263C File Offset: 0x0005083C
		[ClientRpc]
		private void AddRipple(Vector3 position, float strength)
		{
			if (!this.AddRipple__RpcProxy(position, strength, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			WaterSceneObject waterSceneObject = this.WaterSceneObject;
			if (waterSceneObject == null)
			{
				return;
			}
			waterSceneObject.AddRipple(position, strength);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0005267C File Offset: 0x0005087C
		protected bool AddRipple__RpcProxy(Vector3 position, float strength, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("AddRipple", new object[]
				{
					position,
					strength
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1495466471, this))
			{
				if (!NetRead.IsSupported(position))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] AddRipple is not allowed to use Vector3 for the parameter 'position'!");
					return false;
				}
				writer.Write<Vector3>(position);
				if (!NetRead.IsSupported(strength))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] AddRipple is not allowed to use Single for the parameter 'strength'!");
					return false;
				}
				writer.Write<float>(strength);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00052740 File Offset: 0x00050940
		private void AddRipple(To toTarget, Vector3 position, float strength)
		{
			this.AddRipple__RpcProxy(position, strength, new To?(toTarget));
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00052754 File Offset: 0x00050954
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 1495466471)
			{
				Vector3 __position = read.ReadData<Vector3>(default(Vector3));
				float __strength = 0f;
				__strength = read.ReadData<float>(__strength);
				if (!Prediction.WasPredicted("AddRipple", new object[]
				{
					__position,
					__strength
				}))
				{
					this.AddRipple(__position, __strength);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000527BE File Offset: 0x000509BE
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__WaterMaterial, "WaterMaterial", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000688 RID: 1672
		public WaterController WaterController = new WaterController();

		// Token: 0x04000689 RID: 1673
		public WaterSceneObject WaterSceneObject;

		// Token: 0x0400068F RID: 1679
		private VarGeneric<string> _repback__WaterMaterial = new VarGeneric<string>("materials/shadertest/test_water.vmat");
	}
}
