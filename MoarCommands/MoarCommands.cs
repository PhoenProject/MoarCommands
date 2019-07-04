using Smod2;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.Config;

namespace MoarCommands
{
	[PluginDetails(
		name = "MoarCommands",
		id = "phoenix.moarcommands",
		description = "Adds extra commands for admins to use",
		author = "Phoenix",
		configPrefix = "mc",
		SmodMajor = 3,
		SmodMinor = 5,
		SmodRevision = 0
	)]

	public class MoarCommands : Plugin
	{
		[ConfigOption]
		public readonly string[] addexpRanks = { "owner", "admin" };
		[ConfigOption]
		public readonly string[] lightFlickerRanks = { "owner", "admin" };
		[ConfigOption]
		public readonly bool lightFailureAnnouncement = true;
		[ConfigOption]
		public readonly string[] rslotRanks = { "owner" };
		[ConfigOption]
		public readonly string[] rageStateRanks = { "owner" };

		public override void OnDisable()
		{
			this.Info(this.Details.name + " disabled");
		}

		public override void OnEnable()
		{
			this.Info(this.Details.name + " enabled");
		}

		public override void Register()
		{
			this.AddCommand("addexp", new AddExp(this));
			this.AddCommand("flicker", new Flicker(this));
			this.AddCommand("rslot", new ResSlot(this));

			//SCP 096 rage state commands
			this.AddCommand("panic", new Panic(this));
			this.AddCommand("enrage", new Enrage(this));
			this.AddCommand("cooldown", new Cooldown(this));
			this.AddCommand("docile", new Docile(this));
		}
	}
}
