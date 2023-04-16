using Blazored.LocalStorage;

using Fluxor;

using FluentValidation;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using SciMaterials.Contracts.Identity.API;
using SciMaterials.Contracts.WebApi.Clients.Authors;
using SciMaterials.UI.BWASM;
using SciMaterials.UI.BWASM.Extensions;
using SciMaterials.UI.BWASM.Models;
using SciMaterials.UI.BWASM.Models.Validations;
using SciMaterials.UI.BWASM.Services;
using SciMaterials.UI.BWASM.Services.Identity;
using SciMaterials.UI.BWASM.States.UploadFilesForm;
using SciMaterials.WebApi.Clients.Authors;
using SciMaterials.Contracts.WebApi.Clients.Files;
using SciMaterials.WebApi.Clients.Files;
using SciMaterials.Contracts.WebApi.Clients.Categories;
using SciMaterials.WebApi.Clients.Categories;
using SciMaterials.Contracts.WebApi.Clients.Comments;
using SciMaterials.WebApi.Clients.Comments;
using SciMaterials.Contracts.WebApi.Clients.ContentTypes;
using SciMaterials.WebApi.Clients.ContentTypes;
using SciMaterials.Contracts.WebApi.Clients.Tags;
using SciMaterials.Contracts.WebApi.Clients.Urls;
using SciMaterials.Services.Identity.API;
using SciMaterials.WebApi.Clients.Tags;
using SciMaterials.WebApi.Clients.Urls;
using SciMaterials.WebApi.Clients.Resources;
using SciMaterials.Contracts.WebApi.Clients.Resources;

const string materialsApiRoot = "http://localhost:5048";
const string identityApiRoot = "http://localhost:5294";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Nuget
builder.Services
    .AddMudServices()
    .AddBlazoredLocalStorage();

// Api
builder.Services
    .AddScoped<JwtAuthenticationHandler>()

    .AddApiClient<IFilesClient, FilesClient>(materialsApiRoot)
    .AddApiClient<ICategoriesClient, CategoriesClient>(materialsApiRoot)
    .AddApiClient<ICommentsClient, CommentsClient>(materialsApiRoot)
    .AddApiClient<IContentTypesClient, ContentTypesClient>(materialsApiRoot)
    .AddApiClient<ITagsClient, TagsClient>(materialsApiRoot)
    .AddApiClient<IAuthorsClient, AuthorsClient>(materialsApiRoot)
    .AddApiClient<IUrlsClient, UrlsClient>(materialsApiRoot)
    .AddApiClient<IResourcesClient, ResourcesClient>(materialsApiRoot)

    .AddApiClient<IdentityClient>(identityApiRoot, ClientConfiguration)
    .AddScoped<IIdentityApi, IdentityClientOperationDecorator>()
    .AddScoped<IUsersApi, IdentityClientOperationDecorator>()
    .AddScoped<IRolesApi, IdentityClientOperationDecorator>();

// Authentication
builder.Services
    .AddAuthorizationCore()
    .AddScoped<IAuthenticationService, IdentityAuthenticationService>()
    .AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>()
    .AddScoped<IRolesService, IdentityRolesService>()
    .AddScoped<IAccountsService, IdentityAccountsService>()
    //.AddScoped<IAuthoritiesService, TestAuthoritiesService>()
    //.AddSingleton<AuthenticationCache>()
    //.AddSingleton<IAuthorizationHandler, AuthorityHandler>()
    //.AddSingleton<IAuthorizationPolicyProvider, AuthorityPolicyProvider>()
    ;

// Validators // TODO: register with assembly scan
builder.Services
    .AddScoped<IValidator<SignUpForm>, SignUpFormValidator>()
    .AddScoped<IValidator<SignInForm>, SignInFormValidator>()
    .AddScoped<IValidator<UploadFilesFormState>, UploadFilesFormStateValidator>();

// Background
builder.Services
    .AddSingleton<FileUploadScheduleService>();

// MessageBus
builder.Services
    .AddSingleton<ISimpleMessageBus, SimpleMessageBus>();

// State management
builder.Services.AddFluxor(options =>
{
    options.WithLifetime(StoreLifetime.Singleton);
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools(reduxOptions => reduxOptions.Name = "SciMaterials");
});

await builder.Build().RunAsync();

static void ClientConfiguration(IHttpClientBuilder c) => c.AddHttpMessageHandler<JwtAuthenticationHandler>();