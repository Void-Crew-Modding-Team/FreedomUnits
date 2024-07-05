using VoidManager.MPModChecks;

namespace FreedomUnits
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "18107";

        public override string Description => "Allows the user to see the temperature in any units";
    }
}
