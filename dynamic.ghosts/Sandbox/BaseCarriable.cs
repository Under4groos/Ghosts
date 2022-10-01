using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200015D RID: 349
	[Title("Carriable")]
	[Icon("luggage")]
	[Description("An entity that can be carried in the player's inventory and hands.")]
	public class BaseCarriable : AnimatedEntity
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0003EE94 File Offset: 0x0003D094
		public virtual string ViewModelPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003EE97 File Offset: 0x0003D097
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0003EE9F File Offset: 0x0003D09F
		public BaseViewModel ViewModelEntity { get; protected set; }

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UsePhysicsCollision = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHideInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowInFirstPerson = true;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003EEE5 File Offset: 0x0003D0E5
		public virtual bool CanCarry(Entity carrier)
		{
			return true;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003EEE8 File Offset: 0x0003D0E8
		public virtual void OnCarryStart(Entity carrier)
		{
			if (base.IsClient)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetParent(carrier, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = carrier;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003EF24 File Offset: 0x0003D124
		public virtual void SimulateAnimator(PawnAnimator anim)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("aim_body_weight", 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype_handedness", 0);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003EF5D File Offset: 0x0003D15D
		public virtual void SimulateAnimator(CitizenAnimationHelper anim)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.Handedness = CitizenAnimationHelper.Hand.Both;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.AimBodyWeight = 1f;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003EF8C File Offset: 0x0003D18C
		public virtual void OnCarryDrop(Entity dropper)
		{
			if (base.IsClient)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParent(null, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = true;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003EFDC File Offset: 0x0003D1DC
		[Description("This entity has become the active entity. This most likely means a player was carrying it in their inventory and now has it in their hands.")]
		public virtual void ActiveStart(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			Player player = ent as Player;
			if (player != null)
			{
				PawnAnimator animator = player.GetActiveAnimator();
				if (animator != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SimulateAnimator(animator);
				}
			}
			if (this.IsLocalPawn)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateHudElements();
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003F049 File Offset: 0x0003D249
		[Description("This entity has stopped being the active entity. This most likely means a player was holding it but has switched away or dropped it (in which case dropped = true)")]
		public virtual void ActiveEnd(Entity ent, bool dropped)
		{
			if (!dropped)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableDrawing = false;
			}
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003F078 File Offset: 0x0003D278
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			if (base.IsClient && this.ViewModelEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003F0B0 File Offset: 0x0003D2B0
		[Description("Create the viewmodel. You can override this in your base classes if you want to create a certain viewmodel entity.")]
		public virtual void CreateViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateViewModel");
			if (string.IsNullOrEmpty(this.ViewModelPath))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity = new BaseViewModel();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.Owner = this.Owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.EnableViewmodelRendering = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.SetModel(this.ViewModelPath);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003F13D File Offset: 0x0003D33D
		[Description("We're done with the viewmodel - delete it")]
		public virtual void DestroyViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = this.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity = null;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003F161 File Offset: 0x0003D361
		public virtual void CreateHudElements()
		{
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003F163 File Offset: 0x0003D363
		public virtual void DestroyHudElements()
		{
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003F165 File Offset: 0x0003D365
		[Description("Utility - return the entity we should be spawning particles from etc")]
		public virtual ModelEntity EffectEntity
		{
			get
			{
				if (!this.ViewModelEntity.IsValid() || !this.IsFirstPersonMode)
				{
					return this;
				}
				return this.ViewModelEntity;
			}
		}
	}
}
