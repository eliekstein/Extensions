using SweetHelpers.Extensions;
using Xunit;

namespace Extensions.Test
{
    public static class ExtensionsTest
    {
        [Fact]
        public static void Should_encode_64()
        {
            var sut = "https://assets.toggl.com/images/profile.png";
            var result = "aHR0cHM6Ly9hc3NldHMudG9nZ2wuY29tL2ltYWdlcy9wcm9maWxlLnBuZw==";

            Assert.Equal(result, sut.ToBase64String());
        }
    }
}
