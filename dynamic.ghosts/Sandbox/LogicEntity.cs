using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200017E RID: 382
	[Library("ent_logic")]
	[HammerEntity]
	[VisGroup(VisGroup.Logic)]
	[EditorSprite("editor/ent_logic.vmat")]
	[Title("Logic Entity")]
	[Category("Gameplay")]
	[Icon("calculate")]
	[Description("A logic entity that allows to do a multitude of logic operations with Map I/O.<br /><br /> TODO: This is a stop-gap solution and may be removed in the future in favor of \"map blueprints\" or node based Map I/O.")]
	public class LogicEntity : Entity
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00046513 File Offset: 0x00044713
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x0004651B File Offset: 0x0004471B
		[Property]
		[global::DefaultValue(true)]
		[Description("The (initial) enabled state of the logic entity.")]
		public bool Enabled { get; set; } = true;

		// Token: 0x06001212 RID: 4626 RVA: 0x00046524 File Offset: 0x00044724
		[Input]
		[Description("Enables the entity.")]
		public void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00046532 File Offset: 0x00044732
		[Input]
		[Description("Disables the entity, so that it would not fire any outputs.")]
		public void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00046540 File Offset: 0x00044740
		[Input]
		[Description("Toggles the enabled state of the entity.")]
		public void Toggle()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = !this.Enabled;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00046556 File Offset: 0x00044756
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x0004655E File Offset: 0x0004475E
		protected Entity.Output OnMapSpawn { get; set; }

		// Token: 0x06001217 RID: 4631 RVA: 0x00046568 File Offset: 0x00044768
		[Event.Entity.PostSpawnAttribute]
		[Description("Fired after all map entities have spawned, even if it is disabled.")]
		public void OnMapSpawnEvent()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnMapSpawn.Fire(this, 0f);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004658F File Offset: 0x0004478F
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x00046597 File Offset: 0x00044797
		[Description("Fired when the this entity receives the \"Trigger\" input.")]
		protected Entity.Output OnTrigger { get; set; }

		// Token: 0x0600121A RID: 4634 RVA: 0x000465A0 File Offset: 0x000447A0
		[Input]
		[Description("Trigger the \"OnTrigger\" output.")]
		public void Trigger()
		{
			if (!this.Enabled)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTrigger.Fire(this, 0f);
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x000465D0 File Offset: 0x000447D0
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x000465D8 File Offset: 0x000447D8
		[Property]
		[global::DefaultValue(0)]
		[Description("The (initial) value for Variable A")]
		public float VariableA { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x000465E1 File Offset: 0x000447E1
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x000465E9 File Offset: 0x000447E9
		[Property]
		[global::DefaultValue(0)]
		[Description("The (initial) value for Variable B")]
		public float VariableB { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x000465F2 File Offset: 0x000447F2
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x000465FA File Offset: 0x000447FA
		[Description("Fired when the value given to \"CompareInput\" or Variable A (\"Compare\" input) matches our Variable B.")]
		protected Entity.Output OnEqual { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00046603 File Offset: 0x00044803
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0004660B File Offset: 0x0004480B
		[Description("Fired when the value given to \"CompareInput\" or Variable A (\"Compare\" input) is NOT equal our Variable B.")]
		protected Entity.Output OnNotEqual { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00046614 File Offset: 0x00044814
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x0004661C File Offset: 0x0004481C
		[Description("Fired when the value given to \"CompareInput\" or Variable A (\"Compare\" input) is less than our Variable B.")]
		protected Entity.Output OnLessThan { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00046625 File Offset: 0x00044825
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0004662D File Offset: 0x0004482D
		[Description("Fired when the value given to \"CompareInput\" or Variable A (\"Compare\" input) is greater than our Variable B.")]
		protected Entity.Output OnGreaterThan { get; set; }

		// Token: 0x06001227 RID: 4647 RVA: 0x00046638 File Offset: 0x00044838
		[Input]
		[Description("Compares the given number to Variable B and fires the appropriate output.")]
		public void CompareInput(float input)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (input.Equals(this.VariableB))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnEqual.Fire(this, 0f);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnNotEqual.Fire(this, 0f);
			}
			if (input > this.VariableB)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnGreaterThan.Fire(this, 0f);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnLessThan.Fire(this, 0f);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000466D4 File Offset: 0x000448D4
		[Input]
		[Description("Compares Variable A to Variable B and fires the appropriate output.")]
		public void Compare()
		{
			if (!this.Enabled)
			{
				return;
			}
			if (this.VariableA.Equals(this.VariableB))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnEqual.Fire(this, 0f);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnNotEqual.Fire(this, 0f);
			}
			if (this.VariableA > this.VariableB)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnGreaterThan.Fire(this, 0f);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnLessThan.Fire(this, 0f);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00046779 File Offset: 0x00044979
		[Input]
		[Description("Sets the Variable A and fires appropriate outputs.")]
		public void SetVariableA(float input)
		{
			if (!this.Enabled)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VariableA = input;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Compare();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004679B File Offset: 0x0004499B
		[Input]
		[Description("Sets the Variable B and fires appropriate outputs.")]
		public void SetVariableB(float input)
		{
			if (!this.Enabled)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VariableB = input;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Compare();
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000467C0 File Offset: 0x000449C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnMapSpawn = new Entity.Output(this, "OnMapSpawn");
			this.OnTrigger = new Entity.Output(this, "OnTrigger");
			this.OnEqual = new Entity.Output(this, "OnEqual");
			this.OnNotEqual = new Entity.Output(this, "OnNotEqual");
			this.OnLessThan = new Entity.Output(this, "OnLessThan");
			this.OnGreaterThan = new Entity.Output(this, "OnGreaterThan");
			base.CreateHammerOutputs();
		}
	}
}
