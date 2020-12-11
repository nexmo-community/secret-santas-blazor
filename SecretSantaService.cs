using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage;
using Vonage.Messaging;
using Vonage.Verify;

namespace SecretSanta
{
    public class SecretSantaService
    {
        private readonly VonageClient _client;
        private readonly SecretSantaContext _db;
        private readonly IConfiguration _config;

        public SecretSantaService(VonageClient client, SecretSantaContext context, IConfiguration config)
        {
            _client = client;
            _db = context;
            _config = config;
        }

        public async Task<string> StartVeriy(string number)
        {
            return (await _client.VerifyClient.VerifyRequestAsync(
                new VerifyRequest
                {
                    Brand = "North Pole Access",
                    SenderId = _config["VONAGE_NUMBER"],
                    Number = number,
                    WorkflowId = VerifyRequest.Workflow.SMS,
                    PinExpiry = 300
                }
                )).RequestId;
        }

        public async Task<bool> ConfirmCode(string id, string code)
        {
            try
            {
                var result = await _client.VerifyClient.VerifyCheckAsync(new VerifyCheckRequest { Code = code, RequestId = id });
                return true;
            }
            catch (VonageVerifyResponseException)
            {
                return false;
            }
        }

        public async Task ShuffleUsers()
        {
            var rnd = new Random(DateTime.UtcNow.Second);

            var participants = _db.Participants.ToList();
            while (participants.Any(x => !x.HasGiver))
            {
                var participant1 = participants.First(x => !x.HasGiver);
                var unmatched = participants.Where(x => x.Match == null && x.PhoneNumber != participant1.PhoneNumber);
                if (unmatched.Count() == 0)
                {
                    System.Diagnostics.Debug.WriteLine("encountered Edge case");
                    var match = participants.First(x => x.PhoneNumber != participant1.PhoneNumber);
                    participant1.Giver = match;
                    participant1.Match = match.Match;
                    match.Match.Giver = participant1;
                    match.Match = participant1;
                    participant1.HasGiver = true;
                }
                else
                {
                    var match = unmatched.ToList().ElementAt(rnd.Next(unmatched.Count() - 1));
                    participant1.Giver = match;
                    participant1.HasGiver = true;
                    match.Match = participant1;
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task NotifyUsers()
        {
            foreach (var participant in _db.Participants)
            {
                var message = $"Hello {participant.Name} this is Santa. " +
                    $"I'm desperately busy up here at the North Pole and need your help. " +
                    $"Could you help me out and find a gift for {participant.Match.Name}? " +
                    $"It doesn't need to extravagent, I wouldn't spend more than $25." +
                    $"You can send the gift directly to them at: {participant.Match.Address}. ";
                if (!string.IsNullOrEmpty(participant.Match.GiftIdeas))
                {
                    message += $"They wrote me with some ideas of what to get them: {participant.Match.GiftIdeas}";
                }
                await _client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                {
                    To = participant.PhoneNumber,
                    From = _config["VONAGE_NUMBER"],
                    Text = message
                });
                await Task.Delay(1000);
            }
        }

        public async Task NotifyUserOfUpdate(SecretSantaParticipant participant)
        {
            if (participant.Match != null)
            {
                var msg = $"Hello, this is Santa again, just wanted to let you know that your match {participant.Name} sent me some updates:" +
                    $" Their Address is {participant.Address}.";
                if (!string.IsNullOrEmpty(participant.GiftIdeas))
                {
                    msg += $" And they indicated they'd want {participant.GiftIdeas}";
                }
                await _client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                {
                    To = participant.Match.PhoneNumber,
                    From = _config["VONAGE_NUMBER"],
                    Text = msg
                });
            }
        }
    }
}
