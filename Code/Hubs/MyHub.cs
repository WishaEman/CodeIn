using Code.Models;
using Microsoft.AspNetCore.SignalR;

namespace Code.Hubs
{
    public class MyHub: Hub
    {
        public async Task SendMessage(string content, string lang, string compileInput)
        {
            string output = string.Empty;
            if (lang == "cpp")
                output = Compilation.ExecuteCpp(content, compileInput);
            else if (lang == "python")
                output = Compilation.ExecutePython(content, compileInput);

            await Clients.Caller.SendAsync("ReceiveMessage", output);
        }
        public async Task CheckOutput(string content, string lang, string input)
        {
            string output = string.Empty;
            if (lang == "cpp")
                output = Compilation.ExecuteCpp(content, input);
            else if (lang == "python")
                output = Compilation.ExecutePython(content, input);
            bool result = Compilation.Compare(output);

            await Clients.Caller.SendAsync("ReceiveMessage", result);

        }
    }
}
