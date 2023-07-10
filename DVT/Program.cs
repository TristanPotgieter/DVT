using Autofac;
using DVT.Services;
using DVT.Services.Interfaces;

internal partial class Program
{
    private static void Main(string[] args)
    {
        var container = ConfigureContainer();
        var application = container.Resolve<ElevatorService>();
        application.RunElevators();
    }

    private static IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<InputService>().As<IInputService>().SingleInstance();
        builder.RegisterType<UserInteractionService>().As<IUserInteractionService>().SingleInstance();
        builder.RegisterType<ElevatorService>().AsSelf();
        return builder.Build();
    }
}