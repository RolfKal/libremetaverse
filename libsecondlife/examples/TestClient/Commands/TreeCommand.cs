using System;
using System.Collections.Generic;
using System.Text;
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class TreeCommand: Command
    {
        public TreeCommand(TestClient testClient)
		{
			Name = "tree";
			Description = "Rez a tree.";
		}

        public override string Execute(string[] args, LLUUID fromAgentID)
		{
		    if (args.Length == 1)
		    {
		        try
		        {
		            string treeName = args[0].Trim(new char[] { ' ' });
		            Tree tree = (Tree)Enum.Parse(typeof(Tree), treeName);

		            LLVector3 treePosition = Client.Self.SimPosition;
		            treePosition.Z += 3.0f;

		            Client.Objects.AddTree(Client.Network.CurrentSim, new LLVector3(0.5f, 0.5f, 0.5f),
		                LLQuaternion.Identity, treePosition, tree, Client.GroupID, false);

		            return "Attempted to rez a " + treeName + " tree";
		        }
		        catch (Exception)
		        {
		            return "Type !tree for usage";
		        }
		    }

		    string usage = "Usage: !tree [";
		    foreach (string value in Enum.GetNames(typeof(Tree)))
		    {
		        usage += value + ",";
		    }
		    usage = usage.TrimEnd(new char[] { ',' });
		    usage += "]";
		    return usage;
		}
    }
}
