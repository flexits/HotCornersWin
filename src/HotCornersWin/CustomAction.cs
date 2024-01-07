using System.Diagnostics;
using System.Text.Json.Serialization;

namespace HotCornersWin
{
    /// <summary>
    /// A base class for all user-defined hot corner actions.
    /// </summary>
    [JsonDerivedType(typeof(CustomActionShell), "CAShell")]
    [JsonDerivedType(typeof(CustomActionHotkey), "CAHotkey")]
    public abstract class CustomAction
    {
        /// <summary>
        /// Human-readable name. Must be unique.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        [JsonConstructorAttribute]
        public CustomAction() { }

        /// <summary>
        /// Returns an action to be executed.
        /// </summary>
        public abstract Action GetAction();
    }

    /// <summary>
    /// A user-defined action containing a shell command.
    /// </summary>
    public class CustomActionShell : CustomAction
    {
        /// <summary>
        /// Shell command to be executed when this action is run.
        /// </summary>
        public string Command { get; set; }

        public CustomActionShell(string name, string command)
        {
            Name = name;
            Command = command;
        }

        public override Action GetAction()
        {
            return () =>
            {
                _ = new Process
                {
                    StartInfo = new ProcessStartInfo(Command)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            };
        }
    }

    /// <summary>
    /// A user-defined action containing a hotkey combination.
    /// </summary>
    public class CustomActionHotkey : CustomAction
    {
        public List<Keys> MainKeys { get; set; } = new();

        public List<Keys> Modifiers { get; set; } = new();

        public CustomActionHotkey(string name)
        {
            Name = name;
        }

        public override Action GetAction()
        {
            // TODO not implemented
            return () => { };
        }
    }
}
