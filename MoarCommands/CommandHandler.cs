using MEC;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoarCommands
{
	internal class AddExp : ICommandHandler
	{
		private MoarCommands plugin;

		public AddExp(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Adds EXP to 079";
		}
		public string GetUsage()
		{
			return "addexp <Value>";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.addexpRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				int Value;
				bool pass = int.TryParse(args[0], out Value);

				if (pass)
				{
					foreach (Player target in plugin.Server.GetPlayers())
					{
						if (target.TeamRole.Role == Role.SCP_079)
						{
							((GameObject)target.GetGameObject()).GetComponent<Scp079PlayerScript>().AddExperience(Value);
							target.PersonalBroadcast(3, $"You were given {Value} EXP by an admin", false);

							return new[] { $"Added {Value} EXP to SCP 079" };
						}
					}
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
	internal class Flicker : ICommandHandler
	{
		private MoarCommands plugin;

		public Flicker(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Flickers lights in the facility";
		}
		public string GetUsage()
		{
			return "flicker";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.lightFlickerRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				if (plugin.lightFailureAnnouncement) plugin.Server.Map.AnnounceCustomMessage("CAUTION . ERROR IN LIGHT CONTROL SYSTEMS");

				foreach (Room Room in plugin.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType != ZoneType.ENTRANCE))
				{
					Room.FlickerLights();
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
	internal class ResSlot : ICommandHandler
	{
		private MoarCommands plugin;

		public ResSlot(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Grants a reserved slot to the user";
		}

		public string GetUsage()
		{
			return "rslot <PlayerID/SteamID/Name>";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;
			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.rslotRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				foreach (Player target in plugin.Server.GetPlayers(args[0]))
				{
					if (target != null)
					{
						if (!ReservedSlot.ContainsIP(target.IpAddress))
						{
							new ReservedSlot(target.IpAddress, target.SteamId, " " + target.Name + " granted by " + admin.Name).AppendToFile();
							return new[] { target.Name + " Has been given a reserved slot on this server!" };
						}
						else return new[] { "That user already has a reserved slot on this server!" };
					}
				}
			}
			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}

	//SCP 096 rage state commands
	internal class Panic : ICommandHandler
	{
		private MoarCommands plugin;

		public Panic(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Places SCP-096 into a panic state";
		}
		public string GetUsage()
		{
			return "panic";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.rageStateRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				foreach (Player target in plugin.Server.GetPlayers())
				{
					if (target.TeamRole.Role == Role.SCP_096)
					{
						GameObject SCP = (GameObject)target.GetGameObject();

						SCP.GetComponent<Scp096PlayerScript>().ragemultiplier_coodownduration = 0.5f;
						SCP.GetComponent<Scp096PlayerScript>().IncreaseRage(100f);
						SCP.GetComponent<Scp096PlayerScript>().enraged = Scp096PlayerScript.RageState.Panic;
						target.PersonalBroadcast(3, $"You were forcefully paniced by an admin!", false);

						return new[] { $"SCP-096 has been placed into a state of panic" };
					}
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
	internal class Enrage : ICommandHandler
	{
		private MoarCommands plugin;

		public Enrage(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Enrages SCP 096";
		}
		public string GetUsage()
		{
			return "enrage";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.rageStateRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				foreach (Player target in plugin.Server.GetPlayers())
				{
					if (target.TeamRole.Role == Role.SCP_096)
					{
						GameObject SCP = (GameObject)target.GetGameObject();

						SCP.GetComponent<Scp096PlayerScript>().ragemultiplier_coodownduration = 0.5f;
						SCP.GetComponent<Scp096PlayerScript>().IncreaseRage(100f);
						SCP.GetComponent<Scp096PlayerScript>().enraged = Scp096PlayerScript.RageState.Enraged;

						target.PersonalBroadcast(3, $"You were forcefully enraged by an admin!", false);

						return new[] { $"SCP-096 has been enraged" };
					}
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
	internal class Cooldown : ICommandHandler
	{
		private MoarCommands plugin;

		public Cooldown(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Places SCP-096 into the cooldown stage (can not be enraged)";
		}
		public string GetUsage()
		{
			return "cooldown";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.rageStateRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				foreach (Player target in plugin.Server.GetPlayers())
				{
					if (target.TeamRole.Role == Role.SCP_096)
					{
						GameObject SCP = (GameObject)target.GetGameObject();

						SCP.GetComponent<Scp096PlayerScript>().DeductRage();
						SCP.GetComponent<Scp096PlayerScript>().enraged = Scp096PlayerScript.RageState.Cooldown;
						target.PersonalBroadcast(3, $"You were forcefully put into cooldown by an admin!", false);

						return new[] { $"SCP-096 has forced into cooldown" };
					}
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
	internal class Docile : ICommandHandler
	{
		private MoarCommands plugin;

		public Docile(MoarCommands plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Makes SCP-096 docile (can be enraged again instantly)";
		}
		public string GetUsage()
		{
			return "docile";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool valid = sender is Server;
			Player admin = null;

			if (!valid)
			{
				admin = sender as Player;
				if (admin != null)
				{
					valid = plugin.rageStateRanks.Contains(admin.GetRankName());
				}
			}

			if (valid)
			{
				foreach (Player target in plugin.Server.GetPlayers())
				{
					if (target.TeamRole.Role == Role.SCP_096)
					{
						GameObject SCP = (GameObject)target.GetGameObject();

						SCP.GetComponent<Scp096PlayerScript>().ragemultiplier_coodownduration = 0.5f;
						SCP.GetComponent<Scp096PlayerScript>().DeductRage();
						SCP.GetComponent<Scp096PlayerScript>().enraged = Scp096PlayerScript.RageState.Cooldown;
						target.PersonalBroadcast(3, $"You were forcefully made docile by an admin!", false);

						return new[] { $"SCP-096 has been made docile" };
					}
				}
			}

			else if (!valid)
			{
				return new[] { "You are not whitelisted to use this command" };
			}

			return new[] { GetUsage() };
		}
	}
}