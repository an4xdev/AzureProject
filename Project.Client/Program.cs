using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Project.Client;
using Project.Client.Services;
using Project.Shared.DoNotGit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var apiUrl = DontCommit.API_URL;

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddMudServices();

var topicKey = DontCommit.TOPIC_KEY;
var topicName = DontCommit.TOPIC_NAME;

builder.Services.AddSingleton<EventAggregator>();
builder.Services.AddSingleton(new TopicService(topicKey, topicName));

builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();
