using Windows.ApplicationModel.Resources;

namespace DayDayUp.Services;


internal static class ResourceExtensions
{
    public static string GetLocalized(this string resourceKey)
    {
        return _resLoader.GetString(resourceKey);
    }

    private static ResourceLoader _resLoader = new ResourceLoader();
}
