namespace BlazorApp.Shared.Enums
{
    public enum ControllerName
    {
        [HostUrl(HostType = HostType.Users)]
        Empty,
        [HostUrl(HostType = HostType.Users)]
        Auth,
        [HostUrl(HostType = HostType.Users)]
        Permissions, 
        [HostUrl(HostType = HostType.Users)]
        Users,
    }
}