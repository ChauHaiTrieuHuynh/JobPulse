using JobPulse.Service.MapperProfile;
using JobPulse.Worker;
using JobPulse.Worker.Extensions;
using JobPulse.Worker.Workers;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

var builder = Host.CreateApplicationBuilder(args);

#region Firebase Configuration
builder.Services.AddFirebaseService(builder.Configuration);
#endregion

#region DI Configuration
builder.Services.AddJobPulseServiceDI();
#endregion

#region Time Polling Configuration
builder.Services.AddTimePollingService();
#endregion

#region Job Source Configuration
builder.Services.AddJobSourceService(builder.Configuration);
#endregion

#region Email Configuration
builder.Services.AddEmailServices(builder.Configuration);
#endregion

#region AutoMapper Configuration
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile).Assembly);
#endregion

var host = builder.Build();
host.Run();
