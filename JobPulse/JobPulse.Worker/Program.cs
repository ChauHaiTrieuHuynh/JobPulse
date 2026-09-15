using JobPulse.Worker;
using JobPulse.Worker.Extensions;

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

var host = builder.Build();
host.Run();
