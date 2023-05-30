using System.Text.Json.Serialization;

namespace Fer3onBP;

public record Settings
{
    [JsonPropertyName("title")] public string Title { get; set; } = "Solarland™";
    [JsonPropertyName("executable")] public string Executable { get; set; } = "SolarlandClient-Win64-Shipping.exe";
    [JsonPropertyName("productid")] public string ProductId { get; set; }
    [JsonPropertyName("sandboxid")] public string SandboxId { get; set; }
    [JsonPropertyName("deploymentid")] public string DeploymentId { get; set; }

    [JsonPropertyName("requested_splash")]
    public string RequestedSplash { get; set; } = "./EasyAntiCheat/SplashScreen.png";

    [JsonPropertyName("wait_for_game_process_exit")]
    public string WaitForGameProcessExit { get; set; } = "false";
}