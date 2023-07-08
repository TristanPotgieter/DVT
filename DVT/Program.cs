using Autofac;
using DVT.Services;

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

        builder.RegisterType<ElevatorService>().AsSelf();
        builder.RegisterType<InputService>().As<IInputService>().SingleInstance();
        return builder.Build();
    }
}