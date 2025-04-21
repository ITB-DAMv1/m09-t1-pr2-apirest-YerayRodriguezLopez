using Microsoft.AspNetCore.SignalR;

namespace T1PR2_APIREST.Hubs
{
    public class XatHub : Hub
    {
        public async Task EnviaMissatge(string usuari, string missatge)
        {
            await Clients.All.SendAsync("RepMissatge", usuari, missatge);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Nou client: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine($"Client desconnectat: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(ex);
        }
    }
}
